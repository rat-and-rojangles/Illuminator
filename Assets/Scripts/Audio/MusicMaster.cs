using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls music playback. Put this in the first scene.
/// </summary>
public class MusicMaster : MonoBehaviour {

	private static MusicMaster m_staticRef = null;
	public static MusicMaster staticRef {
		get { return m_staticRef; }
	}

	[SerializeField]
	private AudioListener listener;

	public float pitch {
		get { return musicSource.pitch; }
		set { musicSource.pitch = value; }
	}


	[SerializeField]
	private AudioSource musicSource;

	[SerializeField]
	private AudioLowPassFilter lowPassFilter;

	public float lowPassMinCutoff {
		get { return 1350f; }
	}
	public float lpCutoff {
		get { return lowPassFilter.cutoffFrequency; }
		set { lowPassFilter.cutoffFrequency = value; }
	}

	private bool firstLoad = true;
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

#if UNITY_EDITOR
	[ContextMenu ("HaltMusic")]
	private void Halt2 () {
		HaltMusic (1.5f, InterpolationMethod.Sinusoidal);
	}
#endif

	public void HaltMusic (float duration, InterpolationMethod method) {
		StopAllCoroutines ();
		StartCoroutine (HaltMusicHelper (duration, method));
	}

	private IEnumerator HaltMusicHelper (float duration, InterpolationMethod method) {
		float timeElapsed = 0f;
		float initialFreq = lowPassFilter.cutoffFrequency;
		while (timeElapsed <= duration) {
			timeElapsed += Time.deltaTime;
			lowPassFilter.cutoffFrequency = Interpolation.Interpolate (initialFreq, lowPassMinCutoff, timeElapsed / duration, method);
			pitch = Interpolation.Interpolate (1f, 0.75f, timeElapsed / duration, method);
			yield return null;
		}
	}

	/// <summary>
	/// Sets the music to start fading in for some duration.
	/// </summary>
	public void FadeInMusic (float duration, InterpolationMethod method) {
		StopAllCoroutines ();
		StartCoroutine (FadeInMusicHelper (duration, method));
	}
	private IEnumerator FadeInMusicHelper (float duration, InterpolationMethod method) {
		float timeElapsed = 0f;
		float initialFreq = lowPassFilter.cutoffFrequency;
		float initialPitch = pitch;
		while (timeElapsed <= duration) {
			timeElapsed += Time.deltaTime;
			lowPassFilter.cutoffFrequency = Interpolation.Interpolate (initialFreq, 22000f, timeElapsed / duration, method);
			pitch = Interpolation.Interpolate (initialPitch, 1f, timeElapsed / duration, method);
			yield return null;
		}
		lowPassFilter.cutoffFrequency = 22000f;
	}

	void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode) {
		if (!firstLoad) {
			FadeInMusic (1.25f, InterpolationMethod.SquareRoot);
		}
		else {
			firstLoad = false;
		}
	}

	/// <summary>
	/// Removes the filter from the song.
	/// </summary>
	public void RefreshMusic () {
		lowPassFilter.cutoffFrequency = 22000f;
		musicSource.pitch = 1f;
	}
}
