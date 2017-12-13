using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLoseScreen : MonoBehaviour {
	[SerializeField]
	private TutorialController tutorialController;
	public bool successfullyClearedTutorial;
	void OnEnable () {
		if (successfullyClearedTutorial) {
			PlayerRecords.tutorial = false;
		}
		tutorialController.ClearAll ();
	}

}
