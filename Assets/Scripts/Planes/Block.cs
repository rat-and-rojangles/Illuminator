using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// parent is game plane

public class Block : MonoBehaviour {

	[SerializeField]
	private PlaneState m_currentState;
	public PlaneState state {
		get {
			return m_currentState;
		}
		set {
			switch (value) {
				case PlaneState.Primed:
					gameObject.SetActive (true);
					m_solidCollider.enabled = false;
					m_triggerCollider.enabled = true;
					m_meshRenderer.material = Game.staticRef.planeManager.primedMaterial;
					m_currentState = value;
					break;
				case PlaneState.Active:
					m_solidCollider.enabled = true;
					m_triggerCollider.enabled = false;
					m_meshRenderer.material = Game.staticRef.planeManager.activeMaterial;
					m_currentState = value;
					break;
				case PlaneState.Shelved:
					gameObject.SetActive (false);
					m_currentState = value;
					break;
			}
		}
	}

	[SerializeField]
	private BoxCollider2D m_solidCollider;

	[SerializeField]
	private BoxCollider2D m_triggerCollider;

	[SerializeField]
	private MeshRenderer m_meshRenderer;

	private MapSegment m_mapSegment;

	void Start () {
		m_mapSegment = transform.parent.GetComponent<MapSegment> ();
		m_mapSegment.allBlocks.Push (this);
		if (m_mapSegment.plane == Game.staticRef.planeManager.activePlane) {
			m_currentState = PlaneState.Active;
		}
		else if (m_mapSegment.plane == Game.staticRef.planeManager.primedPlane) {
			m_currentState = PlaneState.Primed;
		}
		else {
			m_currentState = PlaneState.Shelved;
		}
		state = m_currentState;
	}
}
