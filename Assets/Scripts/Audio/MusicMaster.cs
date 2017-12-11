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
	private AudioClip [] songs;

	private void SetAudioPair (AudioClip clip) {
		if (clip != musicSource.clip) {
			ForceLowFreq ();
			musicSource.clip = clip;
			musicSource.Play ();
		}
	}

	public void SetMusicByIndex (int index) {
		if (songs.ValidIndex (index)) {
			SetAudioPair (songs [index]);
		}
	}

	public float pitch {
		get { return musicSource.pitch; }
		set { musicSource.pitch = value; }
	}

	private const float volumeReductionFactor = 0.25f;

	[SerializeField]
	private AudioSource musicSource;

	[SerializeField]
	private AudioLowPassFilter lowPassFilter;

	public float haltDuration = 1.25f;
	public InterpolationMethod haltInterpolationMethod = InterpolationMethod.Cubic;

	private const float minFrequency = 600f;

	void Awake () {
		if (m_staticRef != null && m_staticRef != this) {
			Destroy (this.gameObject);
			return;
		}
		else {
			m_staticRef = this;
			DontDestroyOnLoad (this.gameObject);
		}
		SetMusicByIndex (PlayerRecords.musicIndex);
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	[ContextMenu ("HaltMusic")]
	public void HaltMusic () {
		StopAllCoroutines ();
		StartCoroutine (HaltMusicHelper (haltDuration, haltInterpolationMethod));
	}

	private IEnumerator HaltMusicHelper (float duration, InterpolationMethod method) {
		float timeElapsed = 0f;
		float initialFreq = lowPassFilter.cutoffFrequency;
		while (timeElapsed <= duration) {
			timeElapsed += Time.unscaledDeltaTime;
			lowPassFilter.cutoffFrequency = Interpolation.Interpolate (initialFreq, minFrequency, timeElapsed / duration, method);
			pitch = Interpolation.Interpolate (1f, 0.75f, timeElapsed / duration, method);
			// musicSource.volume = Interpolation.Interpolate (volumeReductionFactor, 1f, timeElapsed / duration, method);
			yield return null;
		}
	}

	public void ForceLowFreq () {
		lowPassFilter.cutoffFrequency = minFrequency;
	}

	/// <summary>
	/// Sets the music to start fading in for some duration.
	/// </summary>
	public void FadeInMusic (float targetFrequency = 22000f) {
		StopAllCoroutines ();
		StartCoroutine (FadeInMusicHelper (haltDuration, haltInterpolationMethod, targetFrequency));
	}
	private IEnumerator FadeInMusicHelper (float duration, InterpolationMethod method, float targetFrequency = 22000f) {
		float timeElapsed = 0f;
		float initialFreq = lowPassFilter.cutoffFrequency;
		float initialPitch = pitch;
		while (timeElapsed <= duration) {
			timeElapsed += Time.unscaledDeltaTime;
			lowPassFilter.cutoffFrequency = Interpolation.Interpolate (initialFreq, targetFrequency, timeElapsed / duration, method);
			pitch = Interpolation.Interpolate (initialPitch, 1f, timeElapsed / duration, method);
			yield return null;
		}
	}

	/// <summary>
	/// Fades the music in or out without changing pitch.
	/// </summary>
	public void FadeFrequency (float duration, bool toLow) {
		StopAllCoroutines ();
		StartCoroutine (FadeFrequencyHelper (duration, InterpolationMethod.Cubic, toLow ? minFrequency : 22000f));
	}
	private IEnumerator FadeFrequencyHelper (float duration, InterpolationMethod method, float targetFrequency) {
		float timeElapsed = 0f;
		float initialFreq = lowPassFilter.cutoffFrequency;
		float initialPitch = pitch;
		while (timeElapsed <= duration) {
			timeElapsed += Time.unscaledDeltaTime;
			lowPassFilter.cutoffFrequency = Interpolation.Interpolate (initialFreq, targetFrequency, timeElapsed / duration, method);
			yield return null;
		}
	}

	void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode) {
		if (scene.buildIndex != 0) {
			FadeInMusic ();
		}
		else {
			StopAllCoroutines ();
			StartCoroutine (FadeInMusicHelper (haltDuration * 0.5f, InterpolationMethod.Quadratic, minFrequency));
		}
	}
}
