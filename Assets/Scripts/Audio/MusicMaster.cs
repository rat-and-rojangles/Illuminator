using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicMaster : MonoBehaviour {

	private static MusicMaster m_staticRef = null;
	public static MusicMaster staticRef {
		get { return m_staticRef; }
	}

	[SerializeField]
	private AudioSource musicTrack;

	public AudioLowPassFilter lowPassFilter;

	public float lowPassMinCutoff {
		get { return 350f; }
	}

	private

	void Awake () {
		if (m_staticRef != null && m_staticRef != this) {
			Destroy (this.gameObject);
			return;
		}
		else {
			m_staticRef = this;
			DontDestroyOnLoad (this.gameObject);
		}
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode) {
		RefreshMusic ();
	}

	private void RefreshMusic () {
		if (!musicTrack.isPlaying) {
			musicTrack.Play ();
		}
		lowPassFilter.cutoffFrequency = 22000f;
	}
}
