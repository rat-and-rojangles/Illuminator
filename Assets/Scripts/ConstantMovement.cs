using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour {

	void Update () {
		transform.position += MapSegmentSpawner.AUTO_SCROLL_RATE * Vector3.left * Time.deltaTime;
	}
}
