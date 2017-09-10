using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour {
	private bool m_withinTrigger = false;

	public bool withinTrigger {
		get { return m_withinTrigger; }
	}

	void OnTriggerStay2D (Collider2D col) {
		m_withinTrigger = true;
	}
	void OnTriggerExit2D (Collider2D col) {
		m_withinTrigger = false;
	}
}
