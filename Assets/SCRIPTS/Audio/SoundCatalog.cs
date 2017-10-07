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

	private static SoundCatalog m_staticRef = null;
	public static SoundCatalog staticRef {
		get { return m_staticRef; }
	}
	void Awake () {
		if (m_staticRef != null && m_staticRef != this) {
			Destroy (this.gameObject);
			return;
		}
		else {
			m_staticRef = this;
			//DontDestroyOnLoad (this.gameObject);
		}
	}

	public void PlaySwapSound () {
		audioSource.PlayOneShot (swap);
	}
	public void PlayJumpSound () {
		audioSource.PlayOneShot (jump);
	}
	public void PlayDeathSound () {
		audioSource.PlayOneShot (death);
	}
	public void PlayRandomFootstepSound () {
		audioSource.PlayOneShot (footsteps.RandomElement ());
	}
}
