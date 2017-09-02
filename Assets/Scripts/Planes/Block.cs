using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// parent is game plane

public class Block : MonoBehaviour {

	private PlaneState m_currentState = PlaneState.Active;
	public PlaneState currentState {
		get {
			return m_currentState;
		}
		set {
			switch (value) {
				case PlaneState.Primed:
					if (m_currentState != PlaneState.Primed) {
						myCollider.enabled = false;
						mySpriteRenderer.enabled = true;
						mySpriteRenderer.sprite = PlaneManager.backgroundBlock;
					}
					break;
				case PlaneState.Active:
					if (m_currentState == PlaneState.Primed) {
						myCollider.enabled = true;
						mySpriteRenderer.sprite = PlaneManager.foregroundBlock;
					}
					break;
				case PlaneState.Shelved:
					if (m_currentState == PlaneState.Primed) {
						mySpriteRenderer.enabled = false;
					}
					break;
			}
		}
	}

	[SerializeField]
	private BoxCollider2D myCollider;

	[SerializeField]
	private SpriteRenderer mySpriteRenderer;

	void Awake () {
		//GamePlane myPlane = transform.parent.GetComponent<GamePlane> ();
	}
}
