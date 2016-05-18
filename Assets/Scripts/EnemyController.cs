using UnityEngine;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {

	public float speed;
	public float moveLeft;
	public float moveRight;
	public float wait;

	private bool dirRight = true;
	private Animator animator;
	private Animator playerAnimator;


	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		if (dirRight) {
			
			animator.SetInteger ("Direction", 1);
			animator.speed = 1;

			transform.Translate (Vector2.right * speed * Time.deltaTime);
		} else {
			
			animator.SetInteger ("Direction", 0);
			animator.speed = 1;

			transform.Translate (-Vector2.right * speed * Time.deltaTime);
		}
		if(transform.position.x >= moveRight) {
			dirRight = false;
		}

		if(transform.position.x <= moveLeft) {
			dirRight = true;
		}
	}

	void OnTriggerEnter2D(Collider2D col){

		Console.Write("HERE"); 
		// Tests for collision with Ground tagged objects
		if (col.gameObject.tag == "Boundry" || col.gameObject.tag == "Floor Boundry") {
			Destroy (this);
		}

		if (col.gameObject.tag == "Player") {
			animator.speed = 0;
			Destroy (this);
		}
	}

}
