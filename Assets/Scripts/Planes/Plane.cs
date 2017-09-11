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

	public void ApplyState () {
		foreach (PlaneSegment ps in m_planeSegments) {
			ps.ApplyState ();
		}
	}
}
