using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkUI : MonoBehaviour {
	private Image image;
	private Color baseColor;
	private Color transparentColor;

	public float oneWayDuration;
	public bool fading = false;
	private float elapsedTime = 0f;

	void Awake () {
		image = GetComponent<Image> ();
		baseColor = image.color;
		transparentColor = baseColor.ChangedAlpha (0f);
	}

	void Update () {
		float prevRatio = ratio;
		elapsedTime += Time.unscaledDeltaTime;
		if (prevRatio > ratio) {
			fading = !fading;
		}

		if (fading) {
			image.color = Interpolation.Interpolate (baseColor, transparentColor, ratio, InterpolationMethod.Quadratic);
		}
		else {
			image.color = Interpolation.Interpolate (transparentColor, baseColor, ratio, InterpolationMethod.SquareRoot);
		}

	}

	private float ratio {
		get { return elapsedTime % oneWayDuration; }
	}

	void OnEnable () {
		image.color = transparentColor;
		elapsedTime = 0f;
		fading = false;
	}
}
