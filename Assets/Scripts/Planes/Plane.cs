using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane {

	public event System.Action<PlaneState> OnPlaneSwap;

	private PlaneSegment currentSegment;
	private int currentPlaneSegmentIndex = 0;

	public Plane () {
		currentSegment = Game.staticRef.spawner.possibleSegments.RandomElement ();
		// m_planeSegments = new Queue<PlaneSegment> ();
	}

	public PlaneState state {
		get {
			if (Game.staticRef.planeManager.activePlane == this) {
				return PlaneState.Active;
			}
			else if (Game.staticRef.planeManager.primedPlane == this) {
				return PlaneState.Primed;
			}
			else {
				return PlaneState.Shelved;
			}
		}
	}

	public void SpawnNextColumn () {
		// spawn
		currentSegment.GetColumn (currentPlaneSegmentIndex).Spawn (Game.staticRef.planeManager.rightEdge + 1, this);
		currentPlaneSegmentIndex++;
		if (currentPlaneSegmentIndex >= currentSegment.columnCount) {
			currentPlaneSegmentIndex = 0;
			currentSegment = Game.staticRef.spawner.possibleSegments.RandomElement ();
		}
	}

	public void ApplyState () {
		if (OnPlaneSwap != null) {
			OnPlaneSwap (state);
		}
	}
}
