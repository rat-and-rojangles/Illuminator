using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane {

	public Plane (Color c) {
		m_color = c;
		m_planeSegments = new Queue<PlaneSegment> ();
	}

	private Color m_color;

	/// <summary>
	/// Color associated with this plane.
	/// </summary>
	/// <returns></returns>
	public Color color {
		get { return m_color; }
	}

	private Queue<PlaneSegment> m_planeSegments;
	public Queue<PlaneSegment> planeSegments {
		get { return m_planeSegments; }
	}

	private PlaneSegment lastImpossible;
	/// <summary>
	/// X coordinate for the end of the newest impossible segment. No new impossible segments can be placed before it.
	/// </summary>
	public float furthestImpossibleRightEdge {
		get {
			if (lastImpossible == null) {
				return Mathf.NegativeInfinity;
			}
			else {
				return lastImpossible.transform.position.x + lastImpossible.width + 0.5f;
			}
		}
	}

	public PlaneState state {
		get {
			if (Game.staticRef.planeManager.activePlane == this) {
				return PlaneState.Active;
			}
			else if (Game.staticRef.planeManager.primedPlane == this) {
				return PlaneState.Primed;
			}
			else {
				return PlaneState.Shelved;
			}
		}
	}

	/// <summary>
	/// Register a new plane segment to the right end of this plane.
	/// </summary>
	public void PushSegment (PlaneSegment segment) {
		if (!segment.possible) {
			lastImpossible = segment;
		}
		planeSegments.Enqueue (segment);
	}

	/// <summary>
	/// Remove the leftmost segment.
	/// </summary>
	public void PopSegment () {
		PlaneSegment ps = planeSegments.Dequeue ();
		if (lastImpossible == ps) {
			lastImpossible = null;
		}
		GameObject.Destroy (ps.gameObject);
	}

	public void ApplyState () {
		foreach (PlaneSegment ps in m_planeSegments) {
			ps.ApplyState ();
		}
	}
}
