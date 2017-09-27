using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour {

	private Rigidbody2D rb2d;

	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		rb2d.velocity = Vector2.left * Game.staticRef.AUTO_SCROLL_RATE;
	}
}
