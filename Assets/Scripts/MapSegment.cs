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
public class MapSegment : MonoBehaviour {

	/// <summary>
	/// Rate at which the level scrolls.
	/// </summary>
	public static float AUTO_SCROLL_RATE = 2.0f;

	/// <summary>
	/// Distance from origin to right edge. Alternately, make all prefabs this many blocks wide.
	/// </summary>
	private static float STANDARD_WIDTH = 19f;

	private bool alreadySpawnedAnother = false;

	[SerializeField]
	private GameObject nextRoom;

	// Update is called once per frame
	void Update () {
		if (!alreadySpawnedAnother && transform.position.x <= 0f) {
			alreadySpawnedAnother = true;
			GameObject newRoom = GameObject.Instantiate (nextRoom, transform.parent, true);
			newRoom.name = "MapSegment";
			newRoom.transform.position = transform.position + Vector3.right * STANDARD_WIDTH;
		}
		else if (transform.position.x <= -STANDARD_WIDTH * 1.5f) {
			GameObject.Destroy (this.gameObject);
		}
	}
}
