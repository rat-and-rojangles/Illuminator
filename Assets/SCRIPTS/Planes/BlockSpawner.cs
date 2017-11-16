using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns blocks to each plane
/// </summary>
public class BlockSpawner : MonoBehaviour {
	public PlaneSegment [] possibleSegments;
	public PlaneSegment [] impossibleSegments;

	void Awake () {
		possibleSegments = Resources.LoadAll<PlaneSegment> ("PlaneSegments/Possible");
		impossibleSegments = Resources.LoadAll<PlaneSegment> ("PlaneSegments/Impossible");
	}


	void Update () {
		if (Game.staticRef.planeManager.rightEdge < Game.staticRef.boundaries.spawnLineX) {
			Game.staticRef.planeManager.activePlane.SpawnNextColumn ();
			Game.staticRef.planeManager.primedPlane.SpawnNextColumn ();
			Game.staticRef.planeManager.NextColumn ();
		}
	}
}

