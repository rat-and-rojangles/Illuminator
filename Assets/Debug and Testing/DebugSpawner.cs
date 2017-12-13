using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpawner : MonoBehaviour {
	public GameObject prop;
	public void Spawn () {
		Instantiate (prop);
	}
	public void SpawnWithMessage (string message) {
		Instantiate (prop).GetComponent<DebugShit.NotImplementedThing> ().SetText (message.Replace (';', '\n'));
	}
}
