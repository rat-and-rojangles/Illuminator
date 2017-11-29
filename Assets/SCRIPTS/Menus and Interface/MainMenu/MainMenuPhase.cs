using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPhase : MonoBehaviour {

	MainMenuItem [] menuItems;

	public float cutoffFrequency;

	// Use this for initialization
	void Start () {
		menuItems = GetComponentsInChildren<MainMenuItem> ();
		foreach (MainMenuItem mmi in menuItems) {
			mmi.menuPhase = this;
		}
	}

	public void Shelve () {
		foreach (MainMenuItem mmi in menuItems) {
			mmi.Shelve ();
		}
	}
	public void Reveal () {
		foreach (MainMenuItem mmi in menuItems) {
			mmi.Reveal ();
		}
	}
}
