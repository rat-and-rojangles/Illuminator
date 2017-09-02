using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// One of many planes on a single map segment.
/// </summary>
public class MapSegmentPlane : MonoBehaviour {

	[SerializeField]
	private int m_index;
	/// <summary>
	/// Plane index used to keep multiple segments uniform.
	/// </summary>
	public int index {
		get { return m_index; }
	}

	public PlaneState currentState {
		get {
			if (m_index == myPlaneManager.currentActiveIndex) {
				return PlaneState.Active;
			}
			else if (m_index == myPlaneManager.currentPrimedIndex) {
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

	private PlaneManager myPlaneManager;

	void Awake () {
		myPlaneManager = transform.parent.parent.GetComponent<PlaneManager> ();
	}

	void Start () {
		myPlaneManager.planes [m_index].Enqueue (this);
		if (currentState == PlaneState.Active) {
			SetTransformZ (0f);
		}
		else if (currentState == PlaneState.Primed) {
			SetTransformZ (1f);
		}
		else {
			SetTransformZ (1f);
		}
	}

	private void SetTransformZ (float value) {
		transform.position += Vector3.forward * (-transform.position.z + value);
	}

	public void ChangeState (PlaneState state) {
		bool valid = false;
		switch (state) {
			case PlaneState.Primed:
				if (currentState == PlaneState.Active || currentState == PlaneState.Shelved) {
					SetTransformZ (1f);
					valid = true;
				}
				break;
			case PlaneState.Active:
				if (currentState == PlaneState.Primed) {
					SetTransformZ (0f);
					valid = true;
				}
				break;
			case PlaneState.Shelved:
				if (currentState == PlaneState.Primed) {
					SetTransformZ (1f);
					valid = true;
				}
				break;
		}
		if (valid) {
			foreach (Block b in allBlocks) {
				b.state = state;
			}
		}
	}
}
