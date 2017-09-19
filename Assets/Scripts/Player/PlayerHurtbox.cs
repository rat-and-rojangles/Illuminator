using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour {
	private bool m_withinTrigger = false;
	private bool dead = false;

	public bool withinTrigger {
		get { return m_withinTrigger; }
	}

	void OnTriggerEnter2D (Collider2D col) {
		ProcessTrigger (col);
	}
	void OnTriggerStay2D (Collider2D col) {
		ProcessTrigger (col);
	}
	void OnTriggerExit2D (Collider2D col) {
		m_withinTrigger = false;
	}

	private void ProcessTrigger (Collider2D col) {
		if (col.CompareTag ("Hazardous") && !dead) {
			dead = true;
			Game.staticRef.player.DieFromSlam ();
		}
		m_withinTrigger = true;
	}
}
