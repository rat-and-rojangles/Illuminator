using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Updates the main menu records view.
/// </summary>
public class RecordsText : MonoBehaviour {
	void Awake () {
		GetComponent<UnityEngine.UI.Text> ().text = PlayerRecords.maxDistance.ToString ("f1") + " m max\n" + PlayerRecords.illuminated.ToString ("n0") + " illuminated";
	}

#if UNITY_EDITOR

	[SerializeField]
	private float fakeDistance = 0f;
	[SerializeField]
	private int fakeIlluminated = 0;
	[ContextMenu ("Use fake score values")]
	private void SetFakeScores () {
		PlayerRecords.maxDistance = fakeDistance;
		PlayerRecords.illuminated = fakeIlluminated;
	}

	[ContextMenu ("clear playerprefs")]
	private void ClearPrefs () {
		PlayerPrefs.DeleteAll ();
	}
#endif
}
