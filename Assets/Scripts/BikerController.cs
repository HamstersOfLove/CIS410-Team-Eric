using UnityEngine;
using System.Collections;

public class BikerController : MonoBehaviour {

	public float bikerSpeed;

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

			transform.Translate (Vector2.right * bikerSpeed * Time.deltaTime);
		} else {
			
			animator.SetInteger ("Direction", 0);
			animator.speed = 1;

			transform.Translate (-Vector2.right * bikerSpeed * Time.deltaTime);
		}
		if(transform.position.x >= 5) {
			dirRight = false;
		}

		if(transform.position.x <= 3) {
			dirRight = true;
		}
	}
}
