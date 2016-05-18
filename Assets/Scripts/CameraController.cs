using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private float height;

	// Use this for initialization
	void Start() {

		height = this.gameObject.transform.position.y;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (player.gameObject.transform.position.y > height) {
			this.gameObject.transform.position = new Vector3 (player.gameObject.transform.position.x, player.gameObject.transform.position.y, this.gameObject.transform.position.z);
		} else {
			this.gameObject.transform.position = new Vector3 (player.gameObject.transform.position.x, height, this.gameObject.transform.position.z);
		}
	}
}
