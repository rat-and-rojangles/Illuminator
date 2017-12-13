using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DebugShit {
	public class NotImplementedThing : MonoBehaviour {

		private float duration = 1f;
		private float height = 10f;
		private Vector3 start = new Vector3 (0f, 0f, 1f);

		private TextMesh text;

		void Start () {
			transform.SetParent (Camera.main.transform, false);
			transform.localPosition = start;
			text = GetComponent<TextMesh> ();
			StartCoroutine (Blastoff ());
		}

		public void SetText (string text) {
			GetComponent<TextMesh> ().text = text;
		}

		private IEnumerator Blastoff () {
			Vector3 endpoint = start + Vector3.up * height;
			float timeElapsed = 0f;
			Color endColor = new Color (1f, 1f, 1f, 0f);
			while (timeElapsed < duration) {
				timeElapsed += Time.unscaledDeltaTime;
				transform.localPosition = Interpolation.Interpolate (start, endpoint, timeElapsed / duration, InterpolationMethod.Quadratic);
				text.color = Interpolation.Interpolate (Color.white, endColor, timeElapsed / duration, InterpolationMethod.Quadratic);
				yield return null;
			}
			Destroy (gameObject);
		}

	}
}