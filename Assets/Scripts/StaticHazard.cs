using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHazard : Block {

	void OnCollisionEnter2D (Collision2D other) {
		print ("oj");
		if (other.collider.CompareTag ("Player")) {
			other.collider.GetComponent<PlayerCharacter> ().DieFromSlam ();
		}
	}
}
