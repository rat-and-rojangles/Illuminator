using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlane : MonoBehaviour {

	public enum State { Active, Primed, Shelved }
	private State m_state;

	/// <summary>
	/// This plane's specific index. Specific to its color.
	/// </summary>
	[SerializeField]
	private int planeIndex;

	private BoxCollider2D [] colliders;
	private SpriteRenderer [] spriteRenderers;

	private void SetColliderState (bool value) {
		foreach (BoxCollider2D b in colliders) {
			b.enabled = value;
		}
	}

	// will implement a better way later.
	void Awake () {
		colliders = GetComponentsInChildren<BoxCollider2D> ();
		spriteRenderers = GetComponentsInChildren<SpriteRenderer> ();
	}

	public State myState {
		get { return m_state; }
	}

	// primed to active
	public void Activate () {
		// check if player was rekt
		SetColliderState (true);
		transform.position += Vector3.back;
		// change sprite
	}

	// active to primed
	public void Deactivate () {
		SetColliderState (false);
		transform.position += Vector3.forward;
		// change sprite
	}

	// primed to shelved
	public void Shelve () {
		// disable sprite renderer
	}
}
