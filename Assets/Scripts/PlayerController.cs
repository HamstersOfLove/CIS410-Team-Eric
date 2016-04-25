using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float playerSpeed;
	public Vector2 jumpHeight;

	private Transform button;
	private Animator animator;
	private Rigidbody2D rb;
	private bool isGrounded;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator>();
		isGrounded = true;
	}

	// Update is called once per frame
	void Update()
	{
		
		var horizontal = Input.GetAxis("Horizontal");

		Vector2 movement = new Vector2 (horizontal, 0);

		//Player Movement

		//Left & Right
		if (Input.GetKey (KeyCode.RightArrow)) {
			rb.velocity = movement * playerSpeed;

			animator.SetInteger ("Direction", 0);
			animator.speed = 1;


		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			rb.velocity = movement * playerSpeed;

			animator.SetInteger ("Direction", 1);
			animator.speed = 1;

		} else { 
			//rb.velocity = UnityEngine.Vector2.zero;
			animator.speed = 0;

		}

		//Jump
		if ((Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Space)) && isGrounded) {  

			//button.GetComponent<Button> ().interactable = false;	

			rb.AddForce (jumpHeight, ForceMode2D.Impulse);
			//isGrounded = false;
		}
			

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Ground")) {
			isGrounded = true;
		}
	}

}
