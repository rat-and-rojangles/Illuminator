using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChangeOnAwake : MonoBehaviour {

	[SerializeField]
	private int songIndex;

	void Start () {
		MusicMaster.staticRef.SetActiveSong (songIndex);
	}
}
