using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPassHologram : MonoBehaviour {

	[SerializeField]
	private SpriteRenderer sprite;

	[SerializeField]
	private float maxVisibleDistance;

	private Color startingColor;
	private Color clearColor;

	void Awake () {
		startingColor = sprite.color;
		clearColor = sprite.color.ChangedAlpha (0f);
	}

	void LateUpdate () {
		float distance = transform.position.x - Game.staticRef.player.transform.position.x;
		sprite.color = Interpolation.Interpolate (startingColor, clearColor, distance / maxVisibleDistance, InterpolationMethod.Quadratic);

		transform.position = new Vector3 (transform.position.x, Game.staticRef.player.transform.position.y, transform.position.z);
	}
}
