using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {

	[SerializeField]
	private TextMesh textMesh;

	private float initialX;

	public bool continueUpdating = true;

	void Awake () {
		initialX = transform.position.x - 1f;
	}

	void Update () {
		if (continueUpdating) {
			TextUpdate ();
		}
	}

	private void TextUpdate () {
		textMesh.text = (initialX - transform.position.x + Game.staticRef.player.transform.position.x).ToString ("0.0") + " m";
	}
}
