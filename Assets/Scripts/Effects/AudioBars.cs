using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBars : MonoBehaviour {

	[SerializeField]
	private float maxHeight = 1f;

	public GameObject [] cubes;

	private float [] spectrum = new float [512];
	private float [] bands = new float [8];

	void Update () {
		AudioListener.GetSpectrumData (spectrum, 0, FFTWindow.Hamming);
		MakeFrequencyBands ();
		for (int x = 0; x < 8; x++) {
			float oldHeight = cubes [x].transform.localScale.y;
			float newHeight = bands [x] * maxHeight;
			if (oldHeight > newHeight) {
				newHeight = Interpolation.Interpolate (oldHeight, newHeight, 10 * Time.deltaTime * (oldHeight / maxHeight), InterpolationMethod.SquareRoot);
			}
			cubes [x].transform.localScale = new Vector3 (1f, newHeight, 1f);
		}
	}

	private void MakeFrequencyBands () {
		// 22050 (human hearing range) / 512 samples = 43 hz per sample

		int count = 0;

		for (int i = 0; i < bands.Length; i++) {
			int sampleCount = (int)Mathf.Pow (2, i) * 2;
			if (i == bands.Length - 1) {
				sampleCount += 2;
			}

			float sum = 0f;
			for (int j = 0; j < sampleCount; j++) {
				sum += spectrum [count] * (count + 1);
				count++;
			}
			bands [i] = sum / count;    //average amplitude of that bound
		}


	}

	void Update2 () {
		AudioListener.GetSpectrumData (spectrum, 0, FFTWindow.Rectangular);

		for (int i = 1; i < spectrum.Length - 1; i++) {
			Debug.DrawLine (new Vector3 (i - 1, spectrum [i] + 10, 0), new Vector3 (i, spectrum [i + 1] + 10, 0), Color.red);
			Debug.DrawLine (new Vector3 (i - 1, Mathf.Log (spectrum [i - 1]) + 10, 2), new Vector3 (i, Mathf.Log (spectrum [i]) + 10, 2), Color.cyan);
			Debug.DrawLine (new Vector3 (Mathf.Log (i - 1), spectrum [i - 1] - 10, 1), new Vector3 (Mathf.Log (i), spectrum [i] - 10, 1), Color.green);
			Debug.DrawLine (new Vector3 (Mathf.Log (i - 1), Mathf.Log (spectrum [i - 1]), 3), new Vector3 (Mathf.Log (i), Mathf.Log (spectrum [i]), 3), Color.blue);
		}
	}
}
