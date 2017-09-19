using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterClickToBegin : StateMachineBehaviour {

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		MainMenuCatalog mmc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<MainMenuCatalog> ();
		mmc.logo.SetActive (false);
		mmc.button.SetActive (false);
		Animator tempAnim = mmc.mainCamTransform.GetComponent<Animator> ();
		tempAnim.enabled = false;
		mmc.mainCamTransform.position = new Vector3 (41.5f, -0.97f, -5.11f);
		mmc.mainCamTransform.rotation = Quaternion.identity;
		tempAnim.enabled = true;
		tempAnim.Play ("EnterCharacterSelect");
	}
}
