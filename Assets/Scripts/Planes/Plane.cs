using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane {

	public event System.Action<PlaneState> OnPlaneSwap;

	private PlaneSegment currentSegment;
	private int currentPlaneSegmentIndex = 0;

	private ColumnGenerator columnGenerator;

	public Plane () {
		currentSegment = Game.staticRef.spawner.possibleSegments.RandomElement ();
		columnGenerator = new ColumnGenerator (this);
	}
	public void RegisterOtherPlane (Plane p) {
		columnGenerator.other = p.columnGenerator;
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
		// random implementation
		// float [] blocks = { -5, -4, Random.value < 0.2f ? -1 : 2 };
		// BlockColumn.ConstructNew (blocks).Spawn (Game.staticRef.planeManager.rightEdge + 1, this);

		// serialized implementation
		currentSegment.GetColumn (currentPlaneSegmentIndex).Spawn (Game.staticRef.planeManager.rightEdge + 1, this);
		currentPlaneSegmentIndex++;
		if (currentPlaneSegmentIndex >= currentSegment.columnCount) {
			currentPlaneSegmentIndex = 0;
			currentSegment = Game.staticRef.spawner.possibleSegments.RandomElement ();
		}

		// procedural implementation
		// columnGenerator.SpawnNextColumn ();
	}

	public void ApplyState () {
		if (OnPlaneSwap != null) {
			OnPlaneSwap (state);
		}
	}
}
