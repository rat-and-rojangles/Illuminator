using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour {
	[SerializeField]
	private KeyCode exitKey;
	public void Quit () {
		Application.Quit ();
	}

	void Update () {
		if (Input.GetKeyDown (exitKey)) {
			Quit ();
		}
	}
}
