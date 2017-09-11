using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this on the world object.
/// </summary>
public class PlaneSegmentSpawner : MonoBehaviour {
	[SerializeField]
	private GameObject [] segmentsAvailable;

	public static float SPAWN_BOUNDARY_X = 20f;

	void LateUpdate () {
		for (int x = 0; x < Game.staticRef.planeManager.planes.Length; x++) {
			// Despawn old
			PlaneSegment seg = Game.staticRef.planeManager.planes [x].planeSegments.Peek ();
			if (seg.transform.position.x + seg.width < -SPAWN_BOUNDARY_X) {
				Game.staticRef.planeManager.planes [x].planeSegments.Dequeue ();
				GameObject.Destroy (seg.gameObject);
			}

			// Spawn new
			float furthestRight = Game.staticRef.planeManager.planes [x].planeSegments.Peek ().transform.position.x;
			foreach (PlaneSegment ps in Game.staticRef.planeManager.planes [x].planeSegments) {
				furthestRight += ps.width;
			}
			if (furthestRight < SPAWN_BOUNDARY_X) {
				GameObject newSegment = GameObject.Instantiate (segmentsAvailable.RandomElement (), transform, true);
				newSegment.name = "PlaneSegment";
				newSegment.transform.position = Vector3.right * furthestRight;
				newSegment.GetComponent<PlaneSegment> ().planeIndex = x;
			}
		}
	}
}
