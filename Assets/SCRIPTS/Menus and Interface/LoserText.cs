using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoserText : MonoBehaviour {
	[SerializeField]
	private string [] loserMessages;

	[SerializeField]
	private Text loserMessage;

	[SerializeField]
	private Text illuminated;

	[SerializeField]
	private float mainMenuDelay = 0.5f;

	[SerializeField]
	private GameObject mainMenuButton;

	void Awake () {
		loserMessage.text = loserMessages.RandomElement ();
	}

	/// <summary>
	/// basically, you dead
	/// </summary>
	void OnEnable () {
		illuminated.text = "illuminated: " + Game.staticRef.scoreCounter.illuminatedCount;
		StartCoroutine (RevealMainMenuButton ());
	}

	private IEnumerator RevealMainMenuButton () {
		yield return new WaitForSeconds (mainMenuDelay);
		mainMenuButton.SetActive (true);
	}
}
