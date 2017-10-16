using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instantiates a block on awake.
/// </summary>
public class BlockProxy : MonoBehaviour {
	void Awake () {
		GameObject newBlock = GameObject.Instantiate (Game.staticRef.BLOCK_PREFAB);
		newBlock.transform.parent = transform.parent;
		newBlock.transform.localPosition = transform.localPosition;
		Destroy (gameObject);
	}
}
