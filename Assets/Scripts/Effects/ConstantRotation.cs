using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour {

	[SerializeField]
	public Vector3 eulerPerSecond;

	void Update () {
		//transform.eulerAngles += eulerPerSecond * Time.deltaTime;
		transform.Rotate (eulerPerSecond * Time.deltaTime);
	}
}
