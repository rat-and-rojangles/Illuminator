using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraYFollow : MonoBehaviour {

	[SerializeField]
	private float offset = 0f;

	[SerializeField]
	private float speed = 1f;
	//private InterpolationMethod interpolationMethod;


	void LateUpdate () {
		float playerRatio = Mathf.Clamp01 ((Game.staticRef.player.transform.position.y + 4.5f) / (9.5f + 4.5f));
		//float camRatio = (transform.position.y - 1.5f) / (4.5f - 1.5f);

		float camTarget = playerRatio * (4.5f - 1.5f) + 1.5f;

		Vector3 pos = transform.position;
		pos.y = Mathf.Lerp (pos.y, camTarget, speed * Time.deltaTime);
		transform.SetPosition (pos);
	}
}
