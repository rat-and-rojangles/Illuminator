using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour {

	[SerializeField]
	private Block [] allBlocks;

	/// <summary>
	/// Explode all active blocks.
	/// </summary>
	public void Explode () {
		for (int x = 0; x < allBlocks.Length; x++) {
			if (allBlocks [x].gameObject.activeSelf) {
				allBlocks [x].Explode ();
			}
		}
	}

	/// <summary>
	/// Put a block from the pool here.
	/// </summary>
	public void SpawnBlock (Vector3 position, Plane plane) {
		for (int x = 0; x < allBlocks.Length; x++) {
			if (!allBlocks [x].gameObject.activeSelf) {
				allBlocks [x].SpawnSelf (position, plane);
				return;
			}
		}
#if UNITY_EDITOR
		print ("PUSHING THE BLOCK LIMIT");
#endif
	}
	//
#if UNITY_EDITOR
	[SerializeField]
	private Transform blockParent;
	[SerializeField]
	private GameObject blockPrefab;
	[SerializeField]
	private int blockCount;
	[ContextMenu ("Create Enough Blocks")]
	private void CreateAllBlocks () {
		RegisterBlocks ();
		DropTableBoys ();
		for (int x = 0; x < blockCount; x++) {
			GameObject temp = GameObject.Instantiate (blockPrefab);
			temp.name = "Block";
			temp.SetActive (false);
			temp.transform.parent = blockParent;
		}
		RegisterBlocks ();
	}
	// [ContextMenu ("Register Blocks")]
	private void RegisterBlocks () {
		allBlocks = blockParent.GetComponentsInChildren<Block> (true);
	}

	// [ContextMenu ("DROP TABLE BOYS")]
	/// <summary>
	/// DROP TABLE BOYS
	/// </summary>
	private void DropTableBoys () {
		foreach (Block b in allBlocks) {
			DestroyImmediate (b.gameObject, false);
		}
	}
#endif
}
