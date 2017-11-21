using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {
	[SerializeField]
	private UnityEngine.UI.Text text;

	private float initialX;
	public int illuminatedCount = 0;

	public bool continueUpdating = true;

	public float score {
		get { return transform.position.x - initialX; }
	}

	void Start () {
		initialX = transform.position.x;
		// initialX = Game.staticRef.player.transform.position.x;
	}

	void Update () {
		// if (initialX == null) {
		// 	initialX = Game.staticRef.player.transform.position.x;
		// }
		if (continueUpdating) {
			TextUpdate ();
		}
	}

	private void TextUpdate () {
		// text.text = score.ToString ("0.0") + " m\n"+illuminatedCount+" ill";
		// text.text = illuminatedCount.ToString ();
		text.text = score.ToString ("0.0") + " m";
	}
}
