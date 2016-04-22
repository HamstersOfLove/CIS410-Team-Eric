using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed;

	private Animator animator;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{

		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (horizontal, vertical);

		rb.AddForce (movement * speed);

		if (horizontal > 0)
		{
			animator.SetInteger("Direction", 0);

		}
		else if (horizontal < 0)
		{
			animator.SetInteger("Direction", 1);
		}
	}
}
