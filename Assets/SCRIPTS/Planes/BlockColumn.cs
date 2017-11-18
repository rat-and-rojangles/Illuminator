using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serializable list of block y positions.
/// </summary>
[System.Serializable]
public class BlockColumn {
	public BlockColumn (bool [] blocks) {
		this.blocks = blocks;
	}

	[SerializeField]
	public bool [] blocks;

	public void Spawn (float xPosition, Plane plane) {
		for (int yOffset = 0; yOffset < blocks.Length; yOffset++) {
			if (blocks [yOffset]) {
				Game.staticRef.blockPool.SpawnBlock (new Vector3 (xPosition, yOffset + BlockSpawner.MIN_BLOCK_Y, 0f), plane);
			}
		}
	}
}
