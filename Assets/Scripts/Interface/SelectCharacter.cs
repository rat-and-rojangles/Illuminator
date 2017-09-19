using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour {
	[SerializeField]
	private int characterIndex;

	public void SetCharacter () {
		PlayerPrefs.SetInt ("PlayerTypeIndex", characterIndex);
	}
}
