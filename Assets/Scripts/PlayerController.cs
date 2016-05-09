using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	
	public Vector2 jumpHeight;
	public Text endGame;

	private Animator animator;
	private Rigidbody2D rb;

	private float playerSpeed = 0.067f;
	private bool isJumping = false;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator>();
		endGame.text = "";

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
			}

		}
	
	}
	// Player collision controls
	void OnCollisionEnter2D(Collision2D col){

		// Tests for collision with Ground tagged objects
		if (col.gameObject.tag == "Ground") {
			isJumping = false;
		}

		// Tests for collision with Enemy tagged objects
		else if (col.gameObject.tag == "Enemy") {
			animator.SetInteger ("Direction", 2);

			//animator.speed = 0;
			Destroy (this);
			Application.LoadLevel (2);
		}

		// Tests for collision with Coffee tagged objects
		else if (col.gameObject.tag == "Coffee") {
			col.gameObject.SetActive (false);

			StartCoroutine(SpeedUp ());
		}

		// Tests for collision with End Game objects
		else if (col.gameObject.tag == "End Game") {
			
			StartCoroutine(EndGame ());
		}

	
	}

	void WinText()
	{
		endGame.text = "We did it!";
	}

	// Gives player a 5 second speed boost
	IEnumerator SpeedUp() {
		playerSpeed = 0.125f;
		yield return new WaitForSeconds(5.0f);
		playerSpeed = 0.067f;
	}

	IEnumerator EndGame() {
		yield return new WaitForSeconds(1.0f);
		this.gameObject.SetActive (false);
		WinText ();
	}
}
