using UnityEngine;
using System.Collections;
using System;

public class PlatformMove : MonoBehaviour {

	public float speed;
	public float moveLeft;
	public float moveRight;
	public float wait;

	private bool dirRight = true;
	private Rigidbody2D rb;


	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();

	}

	// Update is called once per frame
	void Update () {
		if (dirRight) {

			transform.Translate (Vector2.right * speed * Time.deltaTime);
		} 
		else {
			transform.Translate (-Vector2.right * speed * Time.deltaTime);
		}
		if(transform.position.x >= moveRight) {
			dirRight = false;
		}

		if(transform.position.x <= moveLeft) {
			dirRight = true;
		}
	}
}
