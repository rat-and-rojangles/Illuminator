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

	/// <summary>
	/// returns 1 if positive, -1 if negative, 0 if zero. epsilon = 0.000001f
	/// </summary>
	public static int Sign (this float x) {
		float epsilon = 0.000001f;
		if (x > epsilon) {
			return 1;
		}
		else if (x < -epsilon) {
			return -1;
		}
		else {
			return 0;
		}
	}

	/// <summary>
	/// If 0, returns 1. Else, returns 0. Intended to swap 0 and 1
	/// </summary>
	public static int Other (this int x) {
		if (x == 0) {
			return 1;
		}
		else {
			return 0;
		}
	}

	/// <summary>
	/// Insert an element before index 0
	/// </summary>
	public static void PushBeginning<T> (this List<T> list, T element) {
		list.Insert (0, element);
	}

	/// <summary>
	/// Remove element at index 0
	/// </summary>
	public static T PopBeginning<T> (this List<T> list) {
		T temp = list [0];
		list.RemoveAt (0);
		return temp;
	}

	/// <summary>
	/// Remove last element
	/// </summary>
	public static T PopLast<T> (this List<T> list) {
		if (list.Count == 0) {
			return default (T);
		}
		else {
			T temp = list [list.Count - 1];
			list.RemoveAt (list.Count - 1);
			return temp;
		}
	}

	/// <summary>
	/// Last element in list
	/// </summary>
	public static T Last<T> (this List<T> list) {
		if (list.Count == 0) {
			return default (T);
		}
		else {
			return list [list.Count - 1];
		}
	}
}
