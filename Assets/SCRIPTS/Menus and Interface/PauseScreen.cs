using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour {

	private bool m_isPaused = false;
	public bool isPaused {
		get { return m_isPaused; }
	}

	[SerializeField]
	private Text infoText;

	[SerializeField]
	private float transitionDuration;

	[HideInInspector]
	[SerializeField]
	private Vector3 activePosition;

	[HideInInspector]
	[SerializeField]
	private Vector3 hiddenPosition;

	/// <summary>
	/// Attempts to pause the game will fail when this is false.
	/// </summary>
	public bool allowPause = true;

	void Start () {
		allowPause = true;
	}

	void Update () {
#if UNITY_EDITOR
		if (Input.GetKeyDown (KeyCode.Escape) && allowPause && !isPaused) {
#else
		if (Utility.SwipedDownThisFrame () && allowPause && !isPaused) {
#endif
			RevealPauseScreen ();
		}

#if UNITY_EDITOR
		else if (Input.GetKeyDown (KeyCode.Escape) && isPaused) {
#else
		else if (Utility.SwipedUpThisFrame ()) {
#endif
			HidePauseScreen ();
		}
	}

	public void RevealPauseScreen () {
		MusicMaster.staticRef.FadeMusic (transitionDuration, 1f, MusicMaster.minFrequency, InterpolationMethod.Quadratic);
		m_isPaused = true;
		Time.timeScale = 0f;
		infoText.text = "max distance: " + PlayerRecords.maxDistance.ToString ("f1") + " m\ncurrent illuminated: " + Game.staticRef.scoreCounter.illuminatedCount.ToString ("n0");
		StopAllCoroutines ();
		StartCoroutine (SlideIntoPlace (activePosition, false));
	}
	public void HidePauseScreen () {
		MusicMaster.staticRef.FadeMusic (transitionDuration, 1f, MusicMaster.maxFrequency, InterpolationMethod.Quadratic);
		m_isPaused = false;
		StopAllCoroutines ();
		StartCoroutine (SlideIntoPlace (hiddenPosition, true));
	}

	private IEnumerator SlideIntoPlace (Vector3 targetPosition, bool unfreezeOnComplete) {
		float timeElapsed = 0f;
		Vector3 initialPosition = transform.position;
		while (timeElapsed < transitionDuration) {
			timeElapsed += Time.unscaledDeltaTime;
			transform.position = Interpolation.Interpolate (initialPosition, targetPosition, timeElapsed / transitionDuration, InterpolationMethod.Quadratic);
			yield return null;
		}
		transform.position = targetPosition;
		if (unfreezeOnComplete) {
			Time.timeScale = 1f;
		}
	}

#if UNITY_EDITOR
	[ContextMenu ("Set Pause Menu Position As Active")]
	private void SetActive () {
		activePosition = transform.position;
	}
	[ContextMenu ("Set Pause Menu Position As Hidden")]
	private void SetHidden () {
		hiddenPosition = transform.position;
	}
	[ContextMenu ("Snap Pause Menu to Active Position")]
	private void SnapActive () {
		transform.position = activePosition;
	}
	[ContextMenu ("Snap Pause Menu to Hidden Position")]
	private void SnapHidden () {
		transform.position = hiddenPosition;
	}
#endif
}
