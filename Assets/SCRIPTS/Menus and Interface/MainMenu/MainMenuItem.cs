using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuItem : MonoBehaviour {
	public MainMenuPhase menuPhase = null;
	public Vector3 activePosition;
	public Vector3 shelvedPosition;

	public Vector2 activeAnchoredPosition;
	public Vector2 shelvedAnchoredPosition;

	private RectTransform rectTransform {
		get { return (RectTransform)transform; }
	}

	public void Shelve () {
		StopAllCoroutines ();
		StartCoroutine (InterpolationHelper2 (shelvedAnchoredPosition));
	}

	public void Reveal () {
		StopAllCoroutines ();
		StartCoroutine (InterpolationHelper2 (activeAnchoredPosition));
	}

	private IEnumerator InterpolationHelper (Vector3 targetPosition) {
		Vector3 startPos = transform.position;
		float elapsedTime = 0f;
		while (elapsedTime < MainMenuController.INTERPOLATION_TIME) {
			elapsedTime += Time.deltaTime;
			transform.position = Vector3.Lerp (startPos, targetPosition, elapsedTime / MainMenuController.INTERPOLATION_TIME);
			yield return null;
		}
		transform.position = targetPosition;
	}

	private IEnumerator InterpolationHelper2 (Vector2 targetPosition) {
		Vector2 startPos = rectTransform.anchoredPosition;
		float elapsedTime = 0f;
		while (elapsedTime < MainMenuController.INTERPOLATION_TIME) {
			elapsedTime += Time.deltaTime;
			rectTransform.anchoredPosition = Vector2.Lerp (startPos, targetPosition, elapsedTime / MainMenuController.INTERPOLATION_TIME);
			yield return null;
		}
		rectTransform.anchoredPosition = targetPosition;
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

	/// <summary>
	/// Converts the serialized transform position into rectTransform anchored position.
	/// </summary>
	[ContextMenu ("Fix Serialization")]
	public void FixSerialization () {
		if (Vector3.Distance (transform.position, activePosition) < Vector3.Distance (transform.position, shelvedPosition)) {
			// currently active
			activeAnchoredPosition = rectTransform.anchoredPosition;
			transform.position = shelvedPosition;
			shelvedAnchoredPosition = rectTransform.anchoredPosition;
			transform.position = activePosition;
		}
		else {
			// currently shelved
			shelvedAnchoredPosition = rectTransform.anchoredPosition;
			transform.position = activePosition;
			activeAnchoredPosition = rectTransform.anchoredPosition;
			transform.position = shelvedPosition;
		}
	}
#endif
}
