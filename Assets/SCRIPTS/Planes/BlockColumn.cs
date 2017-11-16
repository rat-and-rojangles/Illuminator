using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serializable list of block y positions.
/// </summary>
[System.Serializable]
public class BlockColumn {

	[SerializeField]
	private float [] yPositions;

	public void Spawn (float xPosition, Plane plane) {
		foreach (float yPosition in yPositions) {
			float zPosition = 0f;
			if (plane.state == PlaneState.Primed) {
				zPosition = 1f;
			}
			Game.staticRef.blockPool.SpawnBlock (new Vector3 (xPosition, yPosition, zPosition), plane);
		}
	}

#if UNITY_EDITOR
	public static BlockColumn ConstructNew (float [] tempPositions) {
		BlockColumn temp = new BlockColumn ();
		temp.yPositions = tempPositions;
		return temp;
	}

	public float [] yPositionsRevealed {
		get { return yPositions; }
	}

	/// <summary>
	/// Within .2 of each other
	/// </summary>
	private static bool RoughlyEquals (float value1, float value2) {
		return (value2 > value1 - .2f && value2 < value1 + .2f);
	}

	public void RemoveDuplicates () {
		List<float> sortedPositions = new List<float> (yPositions);
		sortedPositions.Sort ();
		for (int x = sortedPositions.Count - 2; x >= 0; x--) {
			while (x + 1 < sortedPositions.Count && RoughlyEquals (sortedPositions [x], sortedPositions [x + 1])) {
				sortedPositions.RemoveAt (x + 1);
			}
		}
		yPositions = sortedPositions.ToArray ();
	}
#endif
}
