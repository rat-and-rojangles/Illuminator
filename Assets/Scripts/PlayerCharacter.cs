﻿using UnityEngine;
using System.Collections;
using Prime31;


public class PlayerCharacter : MonoBehaviour {
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float jumpHeight = 3f;

	private CharacterController2D _controller;
	private Vector3 _velocity;


	void Awake () {
		_controller = GetComponent<CharacterController2D> ();
	}


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update () {
		if (_controller.isGrounded) {
			_velocity.y = 0;
		}

		//_velocity.x = Input.GetAxis ("Horizontal") * runSpeed + MapSegmentSpawner.AUTO_SCROLL_RATE;
		if (Input.GetKey (KeyCode.RightArrow)) {
			_velocity.x = runSpeed + MapSegmentSpawner.AUTO_SCROLL_RATE;
		}
		else if (Input.GetKey (KeyCode.LeftArrow)) {
			_velocity.x = -runSpeed + MapSegmentSpawner.AUTO_SCROLL_RATE;
		}
		else{
			_velocity.x = MapSegmentSpawner.AUTO_SCROLL_RATE;
		}

		// we can only jump from the ground
		// note the kinematic formula
		if (_controller.isGrounded && Input.GetKeyDown (KeyCode.UpArrow)) {
			_velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
		}

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		_controller.move (_velocity * Time.deltaTime);

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
	}

}