using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour {

	[SerializeField]
	private float maxStrength = 0.1f;
	[SerializeField]
	private float duration = 0.1f;
	private float timeRemaining = 0f;

	void Update () {
		// transform.localPosition = Vector3.right * Mathf.Sin (Time.time * 75f) * Mathf.Clamp01 (timeRemaining / duration) * strength;
		transform.localPosition = Random.insideUnitCircle * Mathf.Clamp01 (timeRemaining / duration) * maxStrength;
		timeRemaining -= Time.unscaledDeltaTime;
	}
	public void Shake () {
		timeRemaining = duration;
	}
	public void FinalShake () {
		duration *= 2f;
		maxStrength *= 4f;
		Shake ();
	}
}
