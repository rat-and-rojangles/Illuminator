using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	[SerializeField]
	private MainMenuPhase [] menuPhases;


	public const float INTERPOLATION_TIME = 0.1f;
	public const InterpolationMethod INTERPOLATION_METHOD = InterpolationMethod.SquareRoot;

	private MainMenuPhase currentPhase;

	void Start () {
		currentPhase = menuPhases [0];
	}

	public void StartGame () {
		SceneManager.LoadScene (1);
	}

	public void RevealNewPhase (MainMenuPhase newPhase) {
		if (newPhase != currentPhase) {
			StopAllCoroutines ();
			StartCoroutine (RevealHelper (newPhase));
		}
	}

	private IEnumerator RevealHelper (MainMenuPhase newPhase) {
		currentPhase.Shelve ();
		currentPhase = newPhase;
		yield return new WaitForSeconds (INTERPOLATION_TIME);
		newPhase.Reveal ();
	}

}
