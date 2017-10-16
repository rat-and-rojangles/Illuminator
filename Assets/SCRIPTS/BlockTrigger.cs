using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrigger : MonoBehaviour {
	[SerializeField]
	private Block m_block;

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.name.Equals("TriggerBox")) {
			m_block.ChildTriggerEnter ();
		}
	}
	void OnTriggerExit2D (Collider2D collider) {
		if (collider.name.Equals("TriggerBox")) {
			m_block.ChildTriggerExit ();
		}
	}
}
