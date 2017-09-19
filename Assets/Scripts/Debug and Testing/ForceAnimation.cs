using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAnimation : MonoBehaviour {
	public string animationName;

	[ContextMenu ("Play Animation")]
	private void PlayAnimation () {
		GetComponent<Animator> ().Play (animationName);
	}

	[ContextMenu ("Stop Animation")]
	private void StopAnimation () {
		GetComponent<Animator> ().StopPlayback ();
	}
}
