using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// One of many planes on a single map segment.
/// </summary>
public class MapSegment : MonoBehaviour {
	/// <summary>
	/// The plane associated with this map segment.
	/// </summary>
	/// <returns></returns>
	public Plane plane {
		get { return Game.staticRef.planeManager.planes [m_index]; }
	}

	[SerializeField]
	private int m_index;
	public PlaneState currentState {
		get {
			if (plane == Game.staticRef.planeManager.activePlane) {
				return PlaneState.Active;
			}
			else if (plane == Game.staticRef.planeManager.primedPlane) {
				return PlaneState.Primed;
			}
			else {
				return PlaneState.Shelved;
			}
		}
	}

	/// <summary>
	/// All blocks on this plane.
	/// </summary>
	public Stack<Block> allBlocks = new Stack<Block> ();

	void Start () {
		plane.mapSegments.Enqueue (this);
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
