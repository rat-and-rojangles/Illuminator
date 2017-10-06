using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSegment : MonoBehaviour {
	[SerializeField]
	private float m_width;
	public float width {
		get { return m_width; }
	}

	public float leftEdge {
		get { return transform.position.x; }
	}
	public float rightEdge {
		get { return leftEdge + width; }
	}

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

	[SerializeField]
	private bool m_possible;
	/// <summary>
	/// Can the player cross this without flipping planes?
	/// </summary>
	public bool possible {
		get { return m_possible; }
	}

	private bool initializedAlready = false;
	void Start () {
		if (!initializedAlready) {
			Initialize ();
		}
	}

	public void Initialize () {
		plane.PushSegment (this);
		ApplyState ();
		initializedAlready = true;
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
