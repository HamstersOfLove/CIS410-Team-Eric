using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public static string currentScene;


	public Vector2 jumpHeight;
	public Text endGame;
	public Text diplomaCount;

	private Vector3 movingGround;
	private Animator animator;
	private Rigidbody2D rb;
	private int count = 0;
	private float playerSpeed = 0.065f;
	private bool isJumping = false;

	private string currentLevel;
	private string nextLevel;

	public AudioSource[] sounds;
	private AudioSource jump, powerUP, scrollPickUp, gameOver;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start()
	{

		currentLevel = Application.loadedLevelName;
		currentScene = currentLevel;

		rb = GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator>();

		sounds = this.GetComponents<AudioSource>();
		jump = sounds [0];
		powerUP = sounds [1];
		scrollPickUp = sounds [2];
		gameOver = sounds [3];
		endGame.text = "";
		diplomaCount.text = "Classes Needed: " + (5 - count);
	}

	// Update is called once per frame
	void Update()
	{
		//----------------- Player Movement -----------------
		//Left/Right
		if (Input.GetKey (KeyCode.RightArrow)) { // -------------------------  Right Movement ---
			transform.Translate(Vector2.right * playerSpeed);

			animator.SetInteger ("Direction", 0);
			animator.speed = 1;


		} else if (Input.GetKey (KeyCode.LeftArrow)) { // -------------------  Left Movement ----
			transform.Translate(Vector2.left * playerSpeed);

			animator.SetInteger ("Direction", 1);
			animator.speed = 1;


		} else { // ------------------------------- Stops animation if no key is being held down ---
			animator.speed = 0;
		} 

		//Jump
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (isJumping == false) { // --------------------- Checks for double jumps ---------
				rb.AddForce (jumpHeight, ForceMode2D.Impulse);
				isJumping = true;
				jump.Play();
			}
		}
	}

	// Player collision controls
	void OnCollisionEnter2D(Collision2D col){

		// Tests for collision with Ground tagged objects
		if (col.gameObject.tag == "Ground") {
			isJumping = false;
		}

		if (col.gameObject.tag == "Scroll") {
			endGame.text = "Congrats! Eric, You've Graduated!";
			col.gameObject.SetActive (false);
		}

		// Tests for collision with Enemy tagged objects
		else if (col.gameObject.tag == "Enemy") {
			animator.SetInteger ("Direction", 2);
			StartCoroutine (OnDeath ());
			//animator.speed = 0;
		}

		// Tests for collision with Coffee tagged objects
		else if (col.gameObject.tag == "Coffee") {
			col.gameObject.SetActive (false);
			powerUP.Play ();
			StartCoroutine (SpeedUp ());
		}

		// Tests for collision with Diploma tagged objects
		else if (col.gameObject.tag == "Diploma") {
			col.gameObject.SetActive (false);
			scrollPickUp.Play ();
			count += 1;
			CountText ();
		}

		// Tests for collision with End Game objects (level complete)
		else if (col.gameObject.tag == "End Game") {
			if (count == 5) {

				if (currentLevel == "AdventuresOfEric") {
					print ("Loading Level 2!");
					nextLevel = "Level2";
					WinText ();
					StartCoroutine (LevelTransitionWait ());
				} else if (currentLevel == "Level2") {
					print ("Loading Level 3!");
					nextLevel = "Level3";
					WinText ();
					StartCoroutine (LevelTransitionWait ());
				} else if (currentLevel == "Level3") {
					print ("Loading Level 4!");
					nextLevel = "Level4";
					WinText ();
					StartCoroutine (LevelTransitionWait ());
				} else if (currentLevel == "Level4") {
					print ("Loading Level 5!");
					nextLevel = "Level5";
					WinText ();
					StartCoroutine (LevelTransitionWait ());
				} else if (currentLevel == "Level5") {
					print ("Graduation Day!!!");
					nextLevel = "GraduationDay";
					WinText ();
					StartCoroutine (LevelTransitionWait ());
				}
			} else {
				StartCoroutine (NotFinished ());
			}
		}

		// TODO Not working. Player position does not follow ground
		else if (col.gameObject.tag == "Moving Ground") {
			isJumping = false;
			transform.parent = col.transform;
		}

		// Tests for when player falls off map
		else if (col.gameObject.tag == "Death Box") {
			StartCoroutine (OnDeath ());
		} 

	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.tag == "Moving Ground") {
			transform.parent = null;
		}
	}


	void WinText() // Sets text when player wins level
	{
		endGame.text = "We did it!";
	}

	void CountText () // Sets text when player collects diploma
	{
		if (count < 5) {
			diplomaCount.text = "Classes Needed: " + (5 - count);
		} else {
			diplomaCount.text = "Done! Now go graduate to the next year.";
		}

	}

	IEnumerator SpeedUp() { // Called when player achieves a speed up power up
		playerSpeed = 0.095f;
		yield return new WaitForSeconds(5.0f);
		playerSpeed = 0.065f;
	}

	IEnumerator LevelTransitionWait() {
		print ("Transition Wait: " + nextLevel);
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel (nextLevel);
	}


	IEnumerator OnDeath() { // Called when player dies
		gameOver.Play ();
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel ("Gameover");

	}
	IEnumerator NotFinished() { // Called if player hasn't collected all classes

		endGame.text = "You haven't passed all your classes!";
		yield return new WaitForSeconds(2f);
		endGame.text = "";

	}
}
