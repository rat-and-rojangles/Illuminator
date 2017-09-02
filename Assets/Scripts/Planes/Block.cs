using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// parent is game plane

public class Block : MonoBehaviour {

	private PlaneState m_currentState;
	public PlaneState state {
		get {
			return m_currentState;
		}
		set {
			switch (value) {
				case PlaneState.Primed:
					myCollider.enabled = false;
					mySpriteRenderer.enabled = true;
					mySpriteRenderer.sprite = PlaneManager.backgroundBlock;
					m_currentState = value;
					break;
				case PlaneState.Active:
					myCollider.enabled = true;
					mySpriteRenderer.enabled = true;
					mySpriteRenderer.sprite = PlaneManager.foregroundBlock;
					m_currentState = value;
					break;
				case PlaneState.Shelved:
					myCollider.enabled = false;
					mySpriteRenderer.enabled = false;
					m_currentState = value;
					break;
			}
		}
	}

	[SerializeField]
	private BoxCollider2D myCollider;

	[SerializeField]
	private SpriteRenderer mySpriteRenderer;

	private MapSegmentPlane myPlaneSegment;

	void Awake () {
		myPlaneSegment = transform.parent.GetComponent<MapSegmentPlane> ();
		myPlaneSegment.allBlocks.Push (this);
		if (myPlaneSegment.index == 0) {
			m_currentState = PlaneState.Active;
		}
		else if (myPlaneSegment.index == 1) {
			m_currentState = PlaneState.Primed;
		}
		else {
			m_currentState = PlaneState.Shelved;
		}
	}
}
