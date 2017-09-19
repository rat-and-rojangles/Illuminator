using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCharacter : MonoBehaviour {

	[SerializeField]
	private GameObject [] characters;
	private int m_currentIndex;


	// Use this for initialization
	void Awake () {
		SetIndex (PlayerPrefs.GetInt ("PlayerTypeIndex", 0));
	}

	public void SetIndex (int index) {
		if (index != m_currentIndex) {
			m_currentIndex = index;
			for (int x = 0; x < characters.Length; x++) {
				characters [x].SetActive (x == m_currentIndex);
			}
		}
	}
}
