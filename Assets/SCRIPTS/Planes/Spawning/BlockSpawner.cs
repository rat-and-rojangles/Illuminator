using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns blocks to each plane
/// </summary>
public class BlockSpawner : MonoBehaviour {
	public PlaneSegmentSerialized [] possibleSegments;
	public PlaneSegmentSerialized [] impossibleSegments;

	/// <summary>
	/// The minimum y position for a block on screen.
	/// </summary>
	public static float MIN_BLOCK_Y {
		get { return -5f; }
	}

	void Awake () {
		PlaneSegmentSerialized [] allSegments = Resources.LoadAll<PlaneSegmentSerialized> ("PlaneSegments");
		List<PlaneSegmentSerialized> possibleSegmentsList = new List<PlaneSegmentSerialized> ();
		List<PlaneSegmentSerialized> impossibleSegmentsList = new List<PlaneSegmentSerialized> ();
		foreach (PlaneSegmentSerialized p in allSegments) {
			if (p.possible) {
				possibleSegmentsList.Add (p);
			}
			else {
				impossibleSegmentsList.Add (p);
			}
		}
		possibleSegments = possibleSegmentsList.ToArray ();
		impossibleSegments = impossibleSegmentsList.ToArray ();
	}


	void Update () {
		if (Game.staticRef.planeManager.rightEdge < Game.staticRef.boundaries.spawnLineX) {
			Game.staticRef.planeManager.activePlane.SpawnNextColumn ();
			Game.staticRef.planeManager.primedPlane.SpawnNextColumn ();
			Game.staticRef.planeManager.NextColumn ();
		}
	}
}