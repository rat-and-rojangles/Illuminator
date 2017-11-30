using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPhase : MonoBehaviour {
	[SerializeField]
	private MainMenuController menuController;
	private MainMenuItem [] menuItems;

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

	/// <summary>
	/// Directs the menu manager to do its thing
	/// </summary>
	public void Activate () {
		menuController.RevealNewPhase (this);
	}

#if UNITY_EDITOR
	[ContextMenu ("SetCurrentPositionToActive ALL")]
	public void SetCurrentPositionToActive () {
		foreach (MainMenuItem mmi in GetComponentsInChildren<MainMenuItem> ()) {
			mmi.activePosition = mmi.transform.position;
		}
	}
	[ContextMenu ("SetCurrentPositionToShelved ALL")]
	public void SetCurrentPositionToShelved () {
		foreach (MainMenuItem mmi in GetComponentsInChildren<MainMenuItem> ()) {
			mmi.shelvedPosition = mmi.transform.position;
		}
	}
	[ContextMenu ("SnapToActivePosition ALL")]
	public void SnapToActivePosition () {
		foreach (MainMenuItem mmi in GetComponentsInChildren<MainMenuItem> ()) {
			mmi.transform.position = mmi.activePosition;
		}
	}
	[ContextMenu ("SnapToShelvedPosition ALL")]
	public void SnapToShelvedPosition () {
		foreach (MainMenuItem mmi in GetComponentsInChildren<MainMenuItem> ()) {
			mmi.transform.position = mmi.shelvedPosition;
		}
	}
#endif
}
