using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioBars : MonoBehaviour {

	[SerializeField]
	private float maxHeight = 1f;

	public GameObject [] physicalBars;

	private float [] audioSpectrum = new float [512];
	private float [] audioBands = new float [8];

	void Awake () {
		audioBands = new float [physicalBars.Length];
	}

	void Update () {
		AudioListener.GetSpectrumData (audioSpectrum, 0, FFTWindow.Hamming);
		MakeFrequencyBands ();
		for (int x = 0; x < audioBands.Length; x++) {
			float oldHeight = physicalBars [x].transform.localScale.y;
			float newHeight = audioBands [x] * maxHeight;
			if (oldHeight > newHeight) {
				newHeight = Interpolation.Interpolate (oldHeight, newHeight, 10 * Time.deltaTime * (oldHeight / maxHeight), InterpolationMethod.SquareRoot);
			}
			physicalBars [x].transform.localScale = new Vector3 (1f, newHeight, 1f);
		}
	}

	private void MakeFrequencyBands () {
		// 22050 (human hearing range) / 512 samples = 43 hz per sample

		int count = 0;

		for (int i = 0; i < audioBands.Length; i++) {
			int sampleCount = (int)Mathf.Pow (2, i) * 2;
			if (i == audioBands.Length - 1) {
				sampleCount += 2;
			}

			float sum = 0f;
			for (int j = 0; j < sampleCount; j++) {
				sum += audioSpectrum [count] * (count + 1);
				count++;
			}
			audioBands [i] = sum / count;    //average amplitude of that bound
		}
	}

	void Update2 () {
		AudioListener.GetSpectrumData (audioSpectrum, 0, FFTWindow.Rectangular);

		for (int i = 1; i < audioSpectrum.Length - 1; i++) {
			Debug.DrawLine (new Vector3 (i - 1, audioSpectrum [i] + 10, 0), new Vector3 (i, audioSpectrum [i + 1] + 10, 0), Color.red);
			Debug.DrawLine (new Vector3 (i - 1, Mathf.Log (audioSpectrum [i - 1]) + 10, 2), new Vector3 (i, Mathf.Log (audioSpectrum [i]) + 10, 2), Color.cyan);
			Debug.DrawLine (new Vector3 (Mathf.Log (i - 1), audioSpectrum [i - 1] - 10, 1), new Vector3 (Mathf.Log (i), audioSpectrum [i] - 10, 1), Color.green);
			Debug.DrawLine (new Vector3 (Mathf.Log (i - 1), Mathf.Log (audioSpectrum [i - 1]), 3), new Vector3 (Mathf.Log (i), Mathf.Log (audioSpectrum [i]), 3), Color.blue);
		}
	}

#if UNITY_EDITOR
	[ContextMenu ("Construct Bars")]
	private void ConstructBars () {
		// destroy children
		foreach (Transform child in transform) {
			GameObject.DestroyImmediate (child.gameObject);
		}
		GameObject barPrefab = Resources.Load ("AudioBar") as GameObject;
		for (int x = 0; x < physicalBars.Length; x++) {
			physicalBars [x] = GameObject.Instantiate (barPrefab);
			physicalBars [x].transform.SetParent (transform);
			physicalBars [x].transform.localPosition = Vector3.right * (x - physicalBars.Length / 2f + 0.5f);
			physicalBars [x].transform.localScale = Vector3.one;
			physicalBars [x].transform.localRotation = Quaternion.identity;

		}
	}
#endif
}
