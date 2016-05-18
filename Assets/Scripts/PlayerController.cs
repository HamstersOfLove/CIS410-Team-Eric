using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public static string currentScene;


	public Vector2 jumpHeight;
	public Text endGame;
	public Text diplomaCount;
	public Text speedupCount;
	public Text pushText;

	private Vector3 movingGround;
	private Animator animator;
	private Rigidbody2D rb;
	private int count = 0;
	private int sCount = 0;
	private float playerSpeed = 0.08f;
	private bool isJumping = false;
	private bool isDead = false;

	private string currentLevel;
	private string nextLevel;
	private Scene active;

	public AudioSource[] sounds;
	private AudioSource jump, powerUP, scrollPickUp, gameOver;

	// Use this for initialization
	void Start()
	{

		active = SceneManager.GetActiveScene ();
		currentLevel = active.name;


		currentScene = currentLevel;

		rb = GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator>();

		sounds = this.GetComponents<AudioSource>();
		jump = sounds [0];
		powerUP = sounds [1];
		scrollPickUp = sounds [2];
		gameOver = sounds [3];


		StartCoroutine (BeginningText ());
		diplomaCount.text = "Classes Taken: " + count + "/5";
		speedupCount.text = "Speed Boost: " + sCount;

	}

	// Update is called once per frame
	void Update()
	{
		//----------------- Player Movement -----------------
		//Left/Right

		if (!isDead) {
			if (Input.GetKey (KeyCode.RightArrow)) { // -------------------------  Right Movement ---
				transform.Translate (Vector2.right * playerSpeed);

				animator.SetInteger ("Direction", 0);
				animator.speed = 1;


			} else if (Input.GetKey (KeyCode.LeftArrow)) { // -------------------  Left Movement ----
				transform.Translate (Vector2.left * playerSpeed);

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
					jump.Play ();
				}
			} else if (Input.GetKeyDown (KeyCode.S)) {
				if (sCount > 0) {
					sCount -= 1;
					speedupCount.text = "Speed Boost: " + sCount;
					StartCoroutine (SpeedUp ());
				}
			}
		}
	}

	// Player collision controls
	void OnCollisionEnter2D(Collision2D col){

		// Tests for collision with Ground tagged objects
		if (col.gameObject.tag == "Ground") {
			isJumping = false;
		}



		// Tests for collision with End Game objects (level complete)
		else if (col.gameObject.tag == "End Game") {
			if (count > 0) {

				if (currentLevel == "AdventuresOfEric") {

					nextLevel = "Level2";
					endGame.text = "On To Sophomore Year!";
					StartCoroutine (LevelTransitionWait ());
				} else if (currentLevel == "Level2") {

					nextLevel = "Level3";
					endGame.text = "Whew! That wasn't so bad! On to Junior Year!";
					StartCoroutine (LevelTransitionWait ());
				} else if (currentLevel == "Level3") {

					nextLevel = "Level4";
					endGame.text = "Still another year to go?! Senior Year, here we come..";
					StartCoroutine (LevelTransitionWait ());
				} else if (currentLevel == "Level4") {

					nextLevel = "GraduationDay";
					endGame.text = "Eric! You've done it!!! You're a Wizard!";
					StartCoroutine (LevelTransitionWait ());
				}
			} else {
				StartCoroutine (NotFinished ());
			}
		}
			
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


		
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Scroll") {
			endGame.text = "Congrats! Eric, You've Graduated!";
			col.gameObject.SetActive (false);
		}

		// Tests for collision with Enemy tagged objects
		else if (col.gameObject.tag == "Enemy") {
			
			StartCoroutine (OnDeath ());
			//animator.speed = 0;
		}

		// Tests for collision with Coffee tagged objects
		else if (col.gameObject.tag == "Coffee") {
			col.gameObject.SetActive (false);
			sCount += 1;
			speedupCount.text = "Speed Boost: " + sCount;
			powerUP.Play ();

		}

		// Tests for collision with Diploma tagged objects
		else if (col.gameObject.tag == "Diploma") {
			col.gameObject.SetActive (false);
			scrollPickUp.Play ();
			count += 1;
			CountText ();
		}
	}
	void CountText () // Sets text when player collects diploma
	{
		if (count > 0) {
			diplomaCount.text = "Classes Taken: " + count + "/5";
		} else {
			diplomaCount.text = "Done!";
		}

	}

	IEnumerator BeginningText() { // Called when player achieves a speed up power up
		if (currentLevel == "AdventuresOfEric") {
			endGame.text = "Freshman! Freshman! Freshman!";
			pushText.text = "Push [s] to use speed boost";

		} else if (currentLevel == "Level2") {
			endGame.text = "Sophomoressss!";
			pushText.text = "Push [s] to use speed boost";

		} else if (currentLevel == "Level3") {
			endGame.text = "Junior Year!!!";
			pushText.text = "Push [s] to use speed boost";

		} else if (currentLevel == "Level4") {
			endGame.text = "Seniors Babbbyyyyyy";
			pushText.text = "Push [s] to use speed boost";

		}
		else if (currentLevel == "GraduationDay") {
		endGame.text = "Graduation Day!!!!";
		}
		yield return new WaitForSeconds(3.0f);
		endGame.text = "";
		pushText.text = "";
	}

	IEnumerator SpeedUp() { // Called when player achieves a speed up power up
		playerSpeed = 0.115f;
		yield return new WaitForSeconds(5.0f);
		playerSpeed = 0.08f;
	}

	IEnumerator LevelTransitionWait() {
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
	}


	IEnumerator OnDeath() { // Called when player dies
		gameOver.Play ();
		isDead = true;
		animator.SetInteger ("Direction", 2);
		animator.speed = 1;
		endGame.text = "Game Over!";
		pushText.text = "";
		yield return new WaitForSeconds(2.5f);
		SceneManager.LoadScene("Gameover", LoadSceneMode.Single);


	}
	IEnumerator NotFinished() { // Called if player hasn't collected all classes

		endGame.text = "You haven't passed all your classes!";
		yield return new WaitForSeconds(2f);
		endGame.text = "";

	}
}
