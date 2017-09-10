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
}
