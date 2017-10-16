using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {
	public static Color characterColor;
	public static Color activeBlockColor;
	public static Color primedBlockColor;

	public static Color backgroundColor {
		get {
			// should return active color with half saturation
			return new Color ();
		}
	}
}
