using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	Origin of object should be in the middle.
	Order:
		Is spawned
		Moves
		Spawns another
		Despawns
 */

/// <summary>
/// Effectively like a horizontal room. Used for spawning new rooms.
/// </summary>
public class MapSegmentSpawner : MonoBehaviour {

	/// <summary>
	/// Distance from origin to right edge. Alternately, make all prefabs this many blocks wide.
	/// </summary>
	private static float STANDARD_WIDTH = 19f;

	private bool alreadySpawnedAnother = false;

	[SerializeField]
	private GameObject nextRoom;

	void Update () {
		if (!alreadySpawnedAnother && transform.position.x <= 0f) {
			alreadySpawnedAnother = true;
			GameObject newRoom = GameObject.Instantiate (nextRoom, transform.parent, true);
			newRoom.name = "MapSegment";
			newRoom.transform.position = transform.position + Vector3.right * STANDARD_WIDTH;
		}
		else if (transform.position.x <= -STANDARD_WIDTH * 1.5f) {
			Game.staticRef.planeManager.RemoveLastMapSegment ();
			GameObject.Destroy (this.gameObject);
		}
	}
}
