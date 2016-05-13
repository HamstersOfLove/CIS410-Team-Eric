using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public string prevLevel;

	// Use this for initialization
	void Start () {
		prevLevel = PlayerController.currentScene;

	}

	public void LoadLast()
	{
		SceneManager.LoadScene(prevLevel, LoadSceneMode.Single);
	}


	public void LoadScene(string name) {
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
