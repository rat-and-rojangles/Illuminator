using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSegment : MonoBehaviour {
	public float width;

	public int planeIndex;

	/// <summary>
	/// The plane associated with this map segment.
	/// </summary>
	public Plane plane {
		get { return Game.staticRef.planeManager.planes [planeIndex]; }
	}

	void Start () {
		plane.planeSegments.Enqueue (this);
	}
}
