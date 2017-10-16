using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour {
	[SerializeField]
	private LayerMask slamMask;

	private BoxCollider2D col;

	void Awake () {
		col = GetComponent<BoxCollider2D> ();
	}

	public bool SlamCheck () {
		Collider2D c2 = Physics2D.OverlapBox (col.bounds.center, col.bounds.size, 0f, slamMask);
		return c2 != null;
	}
}
