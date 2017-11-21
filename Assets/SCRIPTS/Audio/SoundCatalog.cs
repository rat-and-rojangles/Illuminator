using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCatalog : MonoBehaviour {
	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip jump;

	[SerializeField]
	private AudioClip [] footsteps;

	[SerializeField]
	private AudioClip swap;
	[SerializeField]
	private AudioClip death;
	[SerializeField]
	private AudioClip speedUp;

	private static SoundCatalog m_staticRef = null;
	public static SoundCatalog staticRef {
		get { return m_staticRef; }
	}
	void Awake () {
		if (m_staticRef != null && m_staticRef != this) {
			// m_staticRef.StopAllSounds ();
			Destroy (this.gameObject);
			return;
		}
		else {
			m_staticRef = this;
			//DontDestroyOnLoad (this.gameObject);
		}
	}

	// private void StopAllSounds () {
	// 	audioSource.Stop ();
	// }

	public void PlaySwapSound () {
		audioSource.PlayOneShot (swap);
	}
	public void PlayJumpSound () {
		audioSource.PlayOneShot (jump);
	}
	public void PlayDeathSound () {
		audioSource.PlayOneShot (death);
	}
	public void PlaySpeedUpSound () {
		audioSource.PlayOneShot (speedUp, 5f);
	}

	// private float footstepResetTime = 0.25f;
	// private float timeOfLastFootstep = 0f;
	// private bool footstepGap = true;
	// private int footstepsIndex = 0;
	// public void PlayRandomFootstepSound () {
	// 	if (Time.time - timeOfLastFootstep > footstepResetTime) {
	// 		footstepGap = false;
	// 	}
	// 	if (!footstepGap) {
	// 		audioSource.PlayOneShot (footsteps [footstepsIndex], 0.25f);
	// 		footstepsIndex = (footstepsIndex + 1) % footsteps.Length;
	// 	}
	// 	footstepGap = !footstepGap;
	// 	timeOfLastFootstep = Time.time;
	// }
}
