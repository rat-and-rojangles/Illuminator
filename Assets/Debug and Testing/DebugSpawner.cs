using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpawner : MonoBehaviour {
	public GameObject prop;
	public void Spawn () {
		Instantiate (prop);
	}
}
