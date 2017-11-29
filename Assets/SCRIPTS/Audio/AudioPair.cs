using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Audio clip with its ideal minimum frequency.
/// </summary>
public class AudioPair : ScriptableObject {
	public AudioClip audioClip;
	public float minFrequency;
}
