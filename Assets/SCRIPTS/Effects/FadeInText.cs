using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInText : MonoBehaviour {
	private Text text;
	public float duration = 1f;
	public InterpolationMethod method;
	void Awake () {
		text = GetComponent<Text> ();
	}
	public void OnEnable () {
		text.color = text.color.ChangedAlpha (0f);
		StartCoroutine (FadeIn ());
	}
	private IEnumerator FadeIn () {
		float timeElapsed = 0f;
		while (timeElapsed < duration) {
			timeElapsed += Time.unscaledDeltaTime;
			text.color = text.color.ChangedAlpha (Interpolation.Interpolate (0f, 1f, timeElapsed / duration, method));
			yield return null;
		}
	}
}
