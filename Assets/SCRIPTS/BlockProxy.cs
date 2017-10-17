using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instantiates a block on awake.
/// </summary>
public class BlockProxy : MonoBehaviour {
	public static float spawnDuration {
		get { return 1f / Game.staticRef.AUTO_SCROLL_RATE; }
	}

	void Update () {
		if (transform.position.x < Game.staticRef.rightSpawnBoundary + 0.6f) {
			Spawn ();
		}
	}

	/// <summary>
	/// Spawn block and remove self.
	/// </summary>
	public void Spawn () {
		GameObject newBlock = GameObject.Instantiate (Game.staticRef.BLOCK_PREFAB);
		newBlock.transform.parent = transform.parent;
		newBlock.transform.localPosition = transform.localPosition;
		Destroy (gameObject);
	}
}
