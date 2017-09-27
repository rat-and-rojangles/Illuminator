using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour {
	private float fixedDeltaTime = 0.0f;
	private float deltaTime = 0.0f;
	private GUIStyle style = new GUIStyle ();
	private Rect rect;

	void Awake () {
		int h = Screen.height / 10;
		rect = new Rect (0, 0, Screen.width, h * 2);
		style.fontSize = h;
		style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
	}

	void FixedUpdate () {
		fixedDeltaTime += (Time.fixedDeltaTime - fixedDeltaTime) * 0.1f;
	}

	void Update () {
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}
	void OnGUI () {
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format ("Update: {0:0.0} ms ({1:0.} fps)", deltaTime * 1000.0f, 1 / deltaTime) + "\n" + string.Format ("FixedUpdate: {0:0.0} ms ({1:0.} fps)", fixedDeltaTime * 1000.0f, 1 / fixedDeltaTime);
		GUI.Label (rect, text, style);
	}
}
