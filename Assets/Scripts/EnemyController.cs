using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed;
	public float moveLeft;
	public float moveRight;
	public float wait;

	private bool dirRight = true;
	private Animator animator;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
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

	void OnCollisionEnter2D(Collision2D col){

		// Tests for collision with Ground tagged objects
		if (col.gameObject.tag == "Boundry" || col.gameObject.tag == "Floor Boundry") {
			Destroy (this);
		}
	}
}
