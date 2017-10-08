using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {

	[SerializeField]
	private TextMesh textMesh;

	private float initialX;

	public bool continueUpdating = true;

	public float score {
		get { return initialX - transform.position.x; }
	}

	void Awake () {
		initialX = transform.position.x;
	}

	void Update () {
		if (continueUpdating) {
			TextUpdate ();
		}
	}

	private void TextUpdate () {
		textMesh.text = score.ToString ("0.0") + " m";
	}
}
