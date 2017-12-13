using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanHalfOfScreen : MonoBehaviour {

	public bool horizontal = true;

	void Start () {
		RectTransform rt = GetComponent<RectTransform> ();
		if (horizontal) {
			rt.sizeDelta = new Vector2 (Screen.width * 0.5f, rt.sizeDelta.y);
		}
		else {
			rt.sizeDelta = new Vector2 (rt.sizeDelta.x, Screen.height * 0.5f);
		}
	}
}
