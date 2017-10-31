using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this on the world object.
/// </summary>
public class PlaneSegmentSpawner : MonoBehaviour {
	[SerializeField]
	private GameObject [] possibleSegments;
	[SerializeField]
	private GameObject [] impossibleSegments;

	void LateUpdate () {
		ProcessPlane (Game.staticRef.planeManager.activePlane, Game.staticRef.planeManager.primedPlane);
		ProcessPlane (Game.staticRef.planeManager.primedPlane, Game.staticRef.planeManager.activePlane);
	}

	private void ProcessPlane (Plane p1, Plane p2) {
		//despawn oldest
		PlaneSegment current = p1.planeSegments.Peek ();
		while (current.rightEdge < Game.staticRef.leftBoundary) {
			p1.DespawnOldestSegment ();
			current = p1.planeSegments.Peek ();
		}

		//Spawn newest
		float furthestRightEdge = p1.planeSegments.Peek ().leftEdge;
		foreach (PlaneSegment ps in p1.planeSegments) {
			furthestRightEdge += ps.width;
		}
		while (furthestRightEdge < Game.staticRef.rightSpawnBoundary) {
			GameObject [] selectedCollection;
			if (furthestRightEdge < p2.furthestImpossibleRightEdge || Random.value < 0.25f) {
				selectedCollection = possibleSegments;
			}
			else {
				selectedCollection = impossibleSegments;
			}

			GameObject newSegObject = GameObject.Instantiate (selectedCollection.RandomElement (), Vector3.right * furthestRightEdge, Quaternion.identity);
			newSegObject.transform.parent = this.transform;
			PlaneSegment newSegComponent = newSegObject.GetComponent<PlaneSegment> ();
			newSegComponent.SetPlane (p1 == Game.staticRef.planeManager.planeA);
			newSegComponent.transform.SetLocalPosition (null, null, p1 == Game.staticRef.planeManager.activePlane ? 1f : 0f);

			p1.PushSegment (newSegComponent);
			furthestRightEdge += newSegComponent.width;
		}
	}
}
