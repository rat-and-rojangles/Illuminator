using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuItem : MonoBehaviour {
	public MainMenuPhase menuPhase = null;
	public Vector3 activePosition;
	public Vector3 shelvedPosition;

	public void Shelve () {
		StopAllCoroutines ();
		StartCoroutine (ShelveHelper ());
	}
	private IEnumerator ShelveHelper () {
		print ("shelving");
		Vector3 startPos = transform.position;
		float elapsedTime = 0f;
		while (elapsedTime < MainMenuController.INTERPOLATION_TIME) {
			elapsedTime += Time.deltaTime;
			transform.position = Vector3.Lerp (startPos, shelvedPosition, elapsedTime / MainMenuController.INTERPOLATION_TIME);
			yield return null;
		}
		transform.position = shelvedPosition;
	}

	public void Reveal () {
		StopAllCoroutines ();
		StartCoroutine (RevealHelper ());
	}
	private IEnumerator RevealHelper () {
		Vector3 startPos = transform.position;
		float elapsedTime = 0f;
		while (elapsedTime < MainMenuController.INTERPOLATION_TIME) {
			elapsedTime += Time.deltaTime;
			transform.position = Vector3.Lerp (startPos, activePosition, elapsedTime / MainMenuController.INTERPOLATION_TIME);
			yield return null;
		}
		transform.position = activePosition;
	}


#if UNITY_EDITOR
	[ContextMenu ("SetCurrentPositionToActive")]
	public void SetCurrentPositionToActive () {
		activePosition = transform.position;
	}
	[ContextMenu ("SetCurrentPositionToShelved")]
	public void SetCurrentPositionToShelved () {
		shelvedPosition = transform.position;
	}
	[ContextMenu ("SnapToActivePosition")]
	public void SnapToActivePosition () {
		transform.position = activePosition;
	}
	[ContextMenu ("SnapToShelvedPosition")]
	public void SnapToShelvedPosition () {
		transform.position = shelvedPosition;
	}
#endif
}
