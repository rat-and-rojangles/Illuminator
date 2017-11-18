using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnGenerator {
	private static float LOWEST_BLOCK = -5f;
	private static float MAX_HEIGHT = 8f;

	private Plane plane;
	public ColumnGenerator (Plane p) {
		plane = p;
	}
	public ColumnGenerator other;

	private int height = 3;
	private int blocksRemainingBeforeChange = 0;

	public void SpawnNextColumn () {
		if (blocksRemainingBeforeChange > 0) {
			blocksRemainingBeforeChange--;
		}
		else {  //recalculate
			int oldHeight = height;
			if (Random.value < 0.75f) { //increase in height
				RerollHeightUp (oldHeight);
				while (!(height - oldHeight < Game.staticRef.player.jumpHeight || other.height - oldHeight < Game.staticRef.player.jumpHeight)) {
					RerollHeightUp (oldHeight);
				}
			}
			else {
				RerollHeightDown ();
			}
			blocksRemainingBeforeChange = Mathf.FloorToInt (Game.staticRef.autoScroller.scrollSpeed * Random.Range (0.5f, 1.5f));
		}

		if (blocksRemainingBeforeChange == other.blocksRemainingBeforeChange) {
			blocksRemainingBeforeChange++;
		}
		Spawn ();
	}

	private void RerollHeightUp (int oldHeight) {
		height = Mathf.FloorToInt (Mathf.Clamp (oldHeight + Random.Range (1, 5), 0, MAX_HEIGHT));
	}
	private void RerollHeightDown () {
		height = Mathf.FloorToInt (Mathf.Clamp (height - Random.Range (1, 10), 0, MAX_HEIGHT));
		// if (height == 0 && Random.value < 0.5f) {
		// 	height = Random.Range (1, 4);
		// }
	}

	private void Spawn () {

		HashSet<float> blocks = new HashSet<float> ();
		// for (int x = 0; x < height; x++) {
		// 	blocks.Add (LOWEST_BLOCK + x);
		// }
		if (height > 0) {
			blocks.Add (LOWEST_BLOCK);
		}
		if (height > 1) {
			blocks.Add (LOWEST_BLOCK + height - 1f);
		}
		BlockColumn.ConstructNew (blocks.ToArray ()).Spawn (Game.staticRef.planeManager.rightEdge + 1, plane);
	}
}
