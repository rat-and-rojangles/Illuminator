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

	/// <summary>
	/// True if the user slid a finger down on the screen.
	/// </summary>
	public static bool SwipedDownThisFrame () {
		foreach (Touch t in Input.touches) {
			if (t.phase == TouchPhase.Moved && t.position.y - t.rawPosition.y < -Screen.height * 0.5f) {
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// True if the user slid a finger up on the screen.
	/// </summary>
	public static bool SwipedUpThisFrame () {
		foreach (Touch t in Input.touches) {
			if (t.phase == TouchPhase.Moved && t.position.y - t.rawPosition.y > Screen.height * 0.5f) {
				return true;
			}
		}
		return false;
	}
}