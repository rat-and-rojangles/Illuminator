using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSegmentSpawner : MonoBehaviour {
	[SerializeField]
	private GameObject segmentsAvailable;

	public static float SPAWN_BOUNDARY_X = 20f;

	void Update () {
		foreach (Plane p in Game.staticRef.planeManager.planes) {
			PlaneSegment seg = p.planeSegments.Peek ();
			if (seg.transform.position.x + seg.width < -SPAWN_BOUNDARY_X) {
				//
			}
		}
	}
}
