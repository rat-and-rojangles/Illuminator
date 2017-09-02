using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour {

	public static Sprite backgroundBlock;
	public static Sprite foregroundBlock;

	private int m_currentActiveIndex = 0;
	public int currentActiveIndex {
		get { return m_currentActiveIndex; }
	}
	public int m_currentPrimedIndex = 1;
	public int currentPrimedIndex {
		get { return m_currentPrimedIndex; }
	}

	public int planesInThisLevel;

	/// <summary>
	/// The index is the plane index. Inside, there is a queue of Map Segment Planes. Popped when the map segment is deleted.
	/// </summary>
	public Queue<MapSegmentPlane> [] planes;

	[SerializeField]
	private Sprite m_backgroundBlock;
	[SerializeField]
	private Sprite m_foregroundBlock;

	void Awake () {
		backgroundBlock = m_backgroundBlock;
		foregroundBlock = m_foregroundBlock;
		planes = new Queue<MapSegmentPlane> [planesInThisLevel];
		for (int x = 0; x < planesInThisLevel; x++) {
			planes [x] = new Queue<MapSegmentPlane> ();
		}
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.S)) {
			Activate ();
		}
	}


	/// <summary>
	/// Swaps the primed and active worlds.
	/// </summary>
	public void Activate () {
		foreach (MapSegmentPlane msp in planes [m_currentActiveIndex]) {
			msp.ChangeState (PlaneState.Primed);
		}
		foreach (MapSegmentPlane msp in planes [m_currentPrimedIndex]) {
			msp.ChangeState (PlaneState.Active);
		}
		int t = m_currentActiveIndex;
		m_currentActiveIndex = m_currentPrimedIndex;
		m_currentPrimedIndex = t;
	}

	public void RemoveLastMapSegment () {
		foreach (Queue<MapSegmentPlane> qmsp in planes) {
			qmsp.Dequeue ();
		}
	}
}
