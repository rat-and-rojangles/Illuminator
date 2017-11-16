using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {
	private UnityEngine.UI.Text text;

	private float initialX;

	public bool continueUpdating = true;

	public float score {
		get { return initialX - transform.position.x; }
	}

	void Awake () {
		initialX = transform.position.x;
		text = GetComponent<UnityEngine.UI.Text> ();
	}

	void Update () {
		if (continueUpdating) {
			TextUpdate ();
		}
	}

	private void TextUpdate () {
		text.text = score.ToString ("0.0") + " m";
	}
}
