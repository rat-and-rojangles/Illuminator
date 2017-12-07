using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	[ContextMenu ("Tooggle pause")]
	private void TogglePauseInstance () {
		isPaused = !isPaused;
	}


	public static bool isPaused {
		get { return Time.timeScale < 0.5f; }
		set {
			if (value != isPaused) {
				if (value) {
					Time.timeScale = 0f;
					MusicMaster.staticRef.HaltMusic ();
				}
				else {
					Time.timeScale = 1f;
					MusicMaster.staticRef.FadeInMusic ();
				}
				// Time.timeScale = value ? 0f : 1f;
			}
		}
	}
}
