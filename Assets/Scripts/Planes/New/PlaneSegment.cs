using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSegment : MonoBehaviour {
	public float width;

	public int planeIndex;

	/// <summary>
	/// All blocks in this plane segment.
	/// </summary>
	public Stack<Block> allBlocks = new Stack<Block> ();

	/// <summary>
	/// The plane this segment is a part of.
	/// </summary>
	public Plane plane {
		get { return Game.staticRef.planeManager.planes [planeIndex]; }
	}

	void Start () {
		plane.planeSegments.Enqueue (this);
		ApplyState ();
	}

	private void SetTransformZ (float value) {
		transform.position += Vector3.forward * (-transform.position.z + value);
	}

	public void ApplyState () {
		if (plane.state == PlaneState.Active) {
			SetTransformZ (0f);
		}
		else {
			SetTransformZ (1f);
		}
		foreach (Block b in allBlocks) {
			b.state = plane.state;
		}
	}
}
