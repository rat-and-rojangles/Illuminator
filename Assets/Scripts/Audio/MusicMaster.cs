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
			// FadeMusic (0f, 1f, minFrequency, InterpolationMethod.Linear);
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

	[SerializeField]
	private AudioSource musicSource;

	[SerializeField]
	private AudioLowPassFilter lowPassFilter;

	public float haltDuration = 1.25f;
	public InterpolationMethod haltInterpolationMethod = InterpolationMethod.Cubic;

	public const float minFrequency = 600f;
	public const float maxFrequency = 22000f;

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

	public void FadeMusic (float duration, float targetPitch, float targetFrequency, InterpolationMethod method) {
		StopAllCoroutines ();
		StartCoroutine (FadeMusicHelper (duration, targetPitch, targetFrequency, method));
	}

	private IEnumerator FadeMusicHelper (float duration, float targetPitch, float targetFrequency, InterpolationMethod method) {
		float timeElapsed = 0f;
		float initialFreq = lowPassFilter.cutoffFrequency;
		float initialPitch = pitch;
		while (timeElapsed <= duration) {
			timeElapsed += Time.unscaledDeltaTime;
			pitch = Interpolation.Interpolate (initialPitch, targetPitch, timeElapsed / duration, method);
			lowPassFilter.cutoffFrequency = Interpolation.Interpolate (initialFreq, targetFrequency, timeElapsed / duration, method);
			yield return null;
		}
	}

	void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode) {
		if (scene.buildIndex != 0) {
			FadeMusic (haltDuration, 1f, maxFrequency, haltInterpolationMethod);
		}
		else {
			FadeMusic (haltDuration * 0.5f, 1f, minFrequency, InterpolationMethod.Quadratic);
		}
	}
}
