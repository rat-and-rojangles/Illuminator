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
	private AudioPair m_audioPair;
	public AudioPair audioPair {
		get { return m_audioPair; }
		set {
			m_audioPair = value;
			ForceLowFreq ();
			musicSource.clip = audioPair.clip;
			musicSource.Play ();
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
	public InterpolationMethod haltInterpolationMethod = InterpolationMethod.Sinusoidal;

	void Awake () {
		if (m_staticRef != null && m_staticRef != this) {
			Destroy (this.gameObject);
			return;
		}
		else {
			m_staticRef = this;
			DontDestroyOnLoad (this.gameObject);
		}
		audioPair = m_audioPair; // play music
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
			timeElapsed += Time.deltaTime;
			lowPassFilter.cutoffFrequency = Interpolation.Interpolate (initialFreq, audioPair.minFrequency, timeElapsed / duration, method);
			pitch = Interpolation.Interpolate (1f, 0.75f, timeElapsed / duration, method);
			// musicSource.volume = Interpolation.Interpolate (volumeReductionFactor, 1f, timeElapsed / duration, method);
			yield return null;
		}
	}

	public void ForceLowFreq () {
		lowPassFilter.cutoffFrequency = m_audioPair.minFrequency;
	}

	/// <summary>
	/// Sets the music to start fading in for some duration.
	/// </summary>
	public void FadeInMusic () {
		StopAllCoroutines ();
		StartCoroutine (FadeInMusicHelper (haltDuration, haltInterpolationMethod));
	}
	private IEnumerator FadeInMusicHelper (float duration, InterpolationMethod method) {
		float timeElapsed = 0f;
		float initialFreq = lowPassFilter.cutoffFrequency;
		float initialPitch = pitch;
		while (timeElapsed <= duration) {
			timeElapsed += Time.deltaTime;
			lowPassFilter.cutoffFrequency = Interpolation.Interpolate (initialFreq, 22000f, timeElapsed / duration, method);
			pitch = Interpolation.Interpolate (initialPitch, 1f, timeElapsed / duration, method);
			// musicSource.volume = Interpolation.Interpolate (1f, volumeReductionFactor, timeElapsed / (duration * 0.75f), method);
			yield return null;
		}
		lowPassFilter.cutoffFrequency = 22000f;
	}

	void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode) {
		if (scene.buildIndex != 0) {
			FadeInMusic ();
		}
		else {
			lowPassFilter.cutoffFrequency = audioPair.minFrequency;
		}
	}
}
