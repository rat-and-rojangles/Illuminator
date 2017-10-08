using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftingHologram : MonoBehaviour {

	public Vector2 animationRate;

	private Vector2 offset = Vector2.zero;
	public Material mat;

	void Awake () {
		Reset ();
	}

	// Update is called once per frame
	void LateUpdate () {
		offset += animationRate * Time.deltaTime;
		mat.SetTextureOffset ("_MainTex", offset);
	}

	[ContextMenu ("reset tiling")]
	private void Reset () {
		mat.SetTextureOffset ("_MainTex", Vector2.zero);
	}
}
