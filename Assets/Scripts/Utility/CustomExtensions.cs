using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomExtensions {

	/// <summary>
	/// Returns a random element from the array.
	/// </summary>
	public static T RandomElement<T> (this T [] array) {
		return array [Random.Range (0, array.Length)];
	}

	/// <summary>
	/// Is this index a valid element for this array?
	/// </summary>
	public static bool WithinBounds<T> (this T [] array, int index) {
		return index >= 0 && index < array.Length;
	}

	/// <summary>
	/// The same color but with a different alpha value.
	/// </summary>
	public static Color ChangedAlpha (this Color c, float a) {
		return new Color (c.r, c.g, c.b, a);
	}

	/// <summary>
	/// non-disruptively set the transform position
	/// </summary>

	public static void SetLocalPosition (this Transform t, float? x, float? y, float? z) {
		float xPrime = x == null ? t.localPosition.x : x.GetValueOrDefault ();
		float yPrime = y == null ? t.localPosition.y : y.GetValueOrDefault ();
		float zPrime = z == null ? t.localPosition.z : z.GetValueOrDefault ();
		t.localPosition += -t.localPosition + new Vector3 (xPrime, yPrime, zPrime);
	}
}
