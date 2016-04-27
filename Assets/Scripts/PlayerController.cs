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


	private bool isJumping = false;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update()
	{
		//Player Movement
		//Left/Right



		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate(Vector2.right * playerSpeed);

			animator.SetInteger ("Direction", 0);
			animator.speed = 1;


		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate(Vector2.left * playerSpeed);

			animator.SetInteger ("Direction", 1);
			animator.speed = 1;

		} else { 
			animator.speed = 0;

		} 

		//Jump
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (isJumping == false) {
				rb.AddForce (jumpHeight, ForceMode2D.Impulse);
				isJumping = true;
			}

		}
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Ground") {
			isJumping = false;
		}
	
	}



}
