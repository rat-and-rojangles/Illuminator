using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public void LoadScene (int sceneIndex) {
		Time.timeScale = 1f;
		SceneManager.LoadScene (sceneIndex);
	}

	public void QuitGame () {
		Application.Quit ();
	}
}
