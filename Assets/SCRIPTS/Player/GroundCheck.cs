using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used on the character for checking if grounded.
/// </summary>
public class GroundCheck : MonoBehaviour {

	public LayerMask groundLayers;

	private bool Check () {

		return Physics2D.OverlapBox (transform.position, transform.localScale, 0f, groundLayers) != null;
	}

	public bool grounded {
		get {
			bool c = Check ();
			print (c);
			return c;
			// return Check ();
		}
	}
}
