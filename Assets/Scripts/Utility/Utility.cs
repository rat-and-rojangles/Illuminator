using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility {

	/// <summary>
	/// Swap two values.
	/// </summary>
	public static void Swap<T> (ref T a, ref T b) {
		T temp = a;
		a = b;
		b = temp;
	}

	public static bool ScreenTappedThisFrame () {
		foreach (Touch t in Input.touches) {
			if (t.phase == TouchPhase.Began) {
				return true;
			}
		}
		return false;
	}
}
