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

	/// <summary>
	/// like a reverse truncate
	/// </summary>
	public static float DecimalPart (float value) {
		return value - ((int)value);
	}
}
