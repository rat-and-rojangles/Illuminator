using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this on the world object.
/// </summary>
public class PlaneSegmentSpawner : MonoBehaviour {
	[SerializeField]
	private GameObject [] segmentsAvailable;


	[SerializeField]
	private float SPAWN_BOUNDARY_X = 20f;

	void LateUpdate () {
		for (int x = 0; x < Game.staticRef.planeManager.planes.Length; x++) {
			// spawn new
			float furthestRight = Game.staticRef.planeManager.planes [x].planeSegments.Peek ().transform.position.x;
			foreach (PlaneSegment ps in Game.staticRef.planeManager.planes [x].planeSegments) {
				furthestRight += ps.width;
			}
			while (furthestRight < SPAWN_BOUNDARY_X) {
				GameObject newSegment = GameObject.Instantiate (segmentsAvailable.RandomElement (), transform, true);
				newSegment.name = "PlaneSegment";
				newSegment.transform.position = Vector3.right * furthestRight;
				PlaneSegment newSegComponent = newSegment.GetComponent<PlaneSegment> ();
				newSegComponent.planeIndex = x;
				furthestRight += newSegComponent.width;
			}
		}
	}
}
