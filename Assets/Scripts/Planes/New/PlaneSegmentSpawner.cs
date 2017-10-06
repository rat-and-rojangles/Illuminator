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
		for (int x = 0; x < Game.staticRef.planeManager.planes.Length; x++) {
			//despawn oldest
			PlaneSegment current = Game.staticRef.planeManager.planes [x].planeSegments.Peek ();
			while (current.rightEdge < Game.staticRef.leftBoundary) {
				Game.staticRef.planeManager.planes [x].PopSegment ();
				current = Game.staticRef.planeManager.planes [x].planeSegments.Peek ();
			}

			//Spawn newest
			float furthestRightEdge = Game.staticRef.planeManager.planes [x].planeSegments.Peek ().leftEdge;
			foreach (PlaneSegment ps in Game.staticRef.planeManager.planes [x].planeSegments) {
				furthestRightEdge += ps.width;
			}
			while (furthestRightEdge < Game.staticRef.rightSpawnBoundary) {
				GameObject [] selectedCollection;
				if (furthestRightEdge < Game.staticRef.planeManager.planes [x.Other ()].furthestImpossibleRightEdge || Random.value < 0.5f) {
					selectedCollection = possibleSegments;
				}
				else {
					selectedCollection = impossibleSegments;
				}

				GameObject newSegObject = GameObject.Instantiate (selectedCollection.RandomElement (), Vector3.right * furthestRightEdge, Quaternion.identity);
				newSegObject.transform.parent = this.transform;
				PlaneSegment newSegComponent = newSegObject.GetComponent<PlaneSegment> ();
				newSegComponent.planeIndex = x;
				newSegComponent.Initialize ();
				furthestRightEdge += newSegComponent.width;
			}
		}
	}
}
