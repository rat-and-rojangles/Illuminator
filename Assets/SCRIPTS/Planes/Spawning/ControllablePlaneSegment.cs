using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllablePlaneSegment : PlaneSegment {

	private static BlockColumn floor = BlockColumn.GenerateColumnOfHeight (3);
	private static BlockColumn obstacle = BlockColumn.GenerateColumnOfHeight (5);
	private static BlockColumn impasseSolid = BlockColumn.GenerateColumnOfHeight (9);
	private static BlockColumn gap = BlockColumn.GenerateColumnOfHeight (0);

	private Queue<BlockColumn> queuedSegments = new Queue<BlockColumn> ();
	private bool impasseOnly = false;

	public bool possible {
		get { return true; }
	}

	public int columnCount {
		get { return int.MaxValue; }
	}

	public BlockColumn GetColumn (int x) {
		if (queuedSegments.Count > 0) {
			return queuedSegments.Dequeue ();
		}
		else if (impasseOnly) {
			return impasseSolid;
		}
		else {
			return floor;
		}
	}

	public void QueueObstacle () {
		queuedSegments.Enqueue (obstacle);
	}
	public void QueueImpasse () {
		queuedSegments.Enqueue (impasseSolid);
	}
	public void QueueGap () {
		queuedSegments.Enqueue (gap);
	}
	public void SetImpasseOnly () {
		impasseOnly = true;
	}
}
