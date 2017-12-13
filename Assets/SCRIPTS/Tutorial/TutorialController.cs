using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {
	private static bool whichOne = true;
	private static Plane plane1 = null;
	private static ControllablePlaneSegment cps1;
	private static ControllablePlaneSegment cps2;

	[SerializeField]
	private GameObject warningText;

	[SerializeField]
	private GameObject realLoserScreen;

	[SerializeField]
	private TutorialPrompt [] allTutorialPrompts;
	private TutorialPrompt tutorialPrompt;

	public static ControllablePlaneSegment GetControllablePlaneSegment (Plane p) {
		whichOne = !whichOne;
		if (whichOne) {
			plane1 = p;
			cps1 = new ControllablePlaneSegment ();
			return cps1;
		}
		else {
			cps2 = new ControllablePlaneSegment ();
			return cps2;
		}
	}

	public void QueueObstaclesActive () {
		ControllablePlaneSegment cp = Game.staticRef.planeManager.activePlane == plane1 ? cps1 : cps2;
		for (int x = 0; x < 3; x++) {
			cp.QueueObstacle ();
		}
	}

	public void QueueImpasseActive () {
		ControllablePlaneSegment cp = Game.staticRef.planeManager.activePlane == plane1 ? cps1 : cps2;
		for (int x = 0; x < 2; x++) {
			cp.QueueImpasse ();
		}
	}

	public void QueueGapActive () {
		ControllablePlaneSegment cp = Game.staticRef.planeManager.activePlane == plane1 ? cps1 : cps2;
		for (int x = 0; x < 3; x++) {
			cp.QueueGap ();
		}
	}

	public void SetWallPrimed () {
		ControllablePlaneSegment cp = Game.staticRef.planeManager.primedPlane == plane1 ? cps1 : cps2;
		cp.SetImpasseOnly ();
	}

	void Start () {
		tutorialPrompt = allTutorialPrompts [PlayerRecords.controlsIndex];
		StartCoroutine (MainCycle ());
	}

	private IEnumerator MainCycle () {
		yield return new WaitForSeconds (3f);

		// jump test
		QueueObstaclesActive ();
		yield return new WaitForSeconds (2.25f);
		tutorialPrompt.jumpPrompt.SetActive (true);
		yield return StartCoroutine (TimeShift (0.25f, 0.5f));

		while (tutorialPrompt.jumpPrompt.activeSelf) {
			yield return null;
		}
		yield return StartCoroutine (TimeShift (1f, 0.5f));

		// swap test
		QueueImpasseActive ();
		yield return new WaitForSeconds (2.25f);
		tutorialPrompt.swapPrompt.SetActive (true);
		yield return StartCoroutine (TimeShift (0.25f, 0.5f));

		while (tutorialPrompt.swapPrompt.activeSelf) {
			yield return null;
		}
		yield return StartCoroutine (TimeShift (1f, 0.5f));

		// aptitude test
		QueueGapActive ();
		yield return new WaitForSeconds (3f);

		// swapWarning
		SetWallPrimed ();
		yield return new WaitForSeconds (3f);
		warningText.SetActive (true);
		Game.staticRef.loseScreen = realLoserScreen;
	}

	private IEnumerator TimeShift (float targetTime, float duration) {
		float timeElapsed = 0f;
		float initialTimeScale = Time.timeScale;
		while (timeElapsed < duration) {
			timeElapsed += Time.unscaledDeltaTime;
			Time.timeScale = Mathf.Lerp (initialTimeScale, targetTime, timeElapsed / duration);
			yield return null;
		}
	}

	public void ClearAll () {
		tutorialPrompt.jumpPrompt.SetActive (false);
		tutorialPrompt.swapPrompt.SetActive (false);
		warningText.SetActive (false);
		StopAllCoroutines ();
		StartCoroutine (TimeShift (1f, 0.25f));
	}
}
