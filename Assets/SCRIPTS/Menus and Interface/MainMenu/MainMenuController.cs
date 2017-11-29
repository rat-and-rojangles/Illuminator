using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// o
/// </summary>
public class MainMenuController : MonoBehaviour {

	[SerializeField]
	private MainMenuPhase [] menuPhases;

	public SuperBlur.SuperBlurFast blur;

	public const float INTERPOLATION_TIME = 0.25f;
	public const InterpolationMethod INTERPOLATION_METHOD = InterpolationMethod.Quadratic;

	private MainMenuPhase currentPhase;

	void Start () {
		// MusicMaster.staticRef.l
	}

}
