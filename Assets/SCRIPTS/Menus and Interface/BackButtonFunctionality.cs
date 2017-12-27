using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonFunctionality : MonoBehaviour {
	[SerializeField]
	private Button currentButton;

	public void SetCurrentButton (Button b) {
		currentButton = b;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Game.staticRef.planeManager.Swap ();
			currentButton.onClick.Invoke ();
		}
	}
}
