using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour {

	[SerializeField]
	public Vector3 eulerPerSecond;

	private Quaternion initialRotation;

	void OnEnable () {
		initialRotation = transform.rotation;
	}
	void OnDisable () {
		transform.rotation = initialRotation;
	}

	void Update () {
		transform.Rotate (eulerPerSecond * Time.deltaTime);
	}
}
