using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane {

	public Plane (Color c) {
		m_color = c;
		m_mapSegments = new Queue<MapSegment> ();
	}

	private Color m_color;

	/// <summary>
	/// Color associated with this plane.
	/// </summary>
	/// <returns></returns>
	public Color color {
		get { return m_color; }
	}

	private Queue<MapSegment> m_mapSegments;
	public Queue<MapSegment> mapSegments {
		get { return m_mapSegments; }
	}

	public Queue<PlaneSegment> planeSegments;

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
		foreach (MapSegment ms in m_mapSegments) {
			ms.ApplyState ();
		}
	}
}
