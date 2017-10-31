using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour {

	// you can despawn blocks by disabling the gameobject
	private Block [] allBlocks;

	/// <summary>
	/// Put a block from the pool here.
	/// </summary>
	public void Spawn (Vector3 position) {
		for (int x = 0; x < allBlocks.Length; x++) {
			if (!allBlocks [x].gameObject.activeSelf) {
				allBlocks [x].transform.position = position;
				// do something to initialize the block
				return;
			}
		}
	}
}
