using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour {

	void Update () {
		transform.position += Game.staticRef.AUTO_SCROLL_RATE * Vector3.left * Time.deltaTime;
	}
}
