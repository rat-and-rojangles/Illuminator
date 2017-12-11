using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {
	[SerializeField]
	private UnityEngine.UI.Text text;

	private float initialX;
	public int illuminatedCount = 0;

	public bool continueUpdating = true;

	private bool newHighScore = false;
	private float prevMaxDistance = 0f;

	public float score {
		get { return transform.position.x - initialX; }
	}

	void Start () {
		initialX = transform.position.x;
		prevMaxDistance = PlayerRecords.maxDistance;
	}

	void Update () {
		if (continueUpdating) {
			TextUpdate ();
			if (!newHighScore) {
				newHighScore = score > prevMaxDistance;
			}
		}
	}

	private void TextUpdate () {
		text.text = score.ToString ("f1") + (newHighScore ? " m!" : " m");
	}

	public void WriteScores () {
		PlayerRecords.illuminated += Game.staticRef.scoreCounter.illuminatedCount;
		PlayerRecords.maxDistance = Mathf.Max (PlayerRecords.maxDistance, score);
	}
}
