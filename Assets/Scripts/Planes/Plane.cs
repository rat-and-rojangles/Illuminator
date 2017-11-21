using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane {

	public event System.Action<PlaneState> OnPlaneSwap;

	private PlaneSegment currentSegment;
	private int currentPlaneSegmentIndex = 0;

	private Plane other = null;

	/// <summary>
	/// Cannot spawn from an impossible segment while this number > 0
	/// </summary>
	private int blocksBeforeImpossible = 0;

	/// <summary>
	/// Likelihood of spawning an impossible segment, when available.s
	/// </summary>
	private float impossibleSegmentProbability {
		get { return 10.75f; }
	}

	public Plane () {
		currentSegment = new EasyPlaneSegment (Mathf.RoundToInt (Game.staticRef.boundaries.spawnLineX * 2f));
	}
	public void RegisterOtherPlane (Plane p) {
		other = p;
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
		// serialized implementation
		currentSegment.GetColumn (currentPlaneSegmentIndex).Spawn (Game.staticRef.planeManager.rightEdge + 1, this);
		currentPlaneSegmentIndex++;
		if (currentPlaneSegmentIndex >= currentSegment.columnCount) {
			currentPlaneSegmentIndex = 0;
			if (blocksBeforeImpossible <= 0 && Random.value < impossibleSegmentProbability) {
				currentSegment = Game.staticRef.spawner.impossibleSegments.RandomElement ();
				other.blocksBeforeImpossible = currentSegment.columnCount;
			}
			else {
				currentSegment = Game.staticRef.spawner.possibleSegments.RandomElement ();
			}
		}
		blocksBeforeImpossible--;
	}

	public void ApplyState () {
		if (OnPlaneSwap != null) {
			OnPlaneSwap (state);
		}
	}
}
