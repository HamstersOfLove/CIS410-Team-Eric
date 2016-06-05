using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditScroller : MonoBehaviour {

	public float scrollSpeed;
	public float tileSize;
	private string nextLevel;

	private Vector2 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSize);
		transform.position = startPosition + Vector2.up * newPosition;

		if (transform.position.y > 7.9) {
			nextLevel = "LevelManager";
			SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
		}
	}
}