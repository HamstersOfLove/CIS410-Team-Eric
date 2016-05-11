using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public string prevLevel;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	 
	// Use this for initialization
	void Start () {
		prevLevel = PlayerController.currentScene;

	}

	public void LoadLast()
	{
		Application.LoadLevel (prevLevel);
	}


	public void LoadScene(string name) {
		Application.LoadLevel (name);
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
