using UnityEngine;

public class TiltWindow : MonoBehaviour {
	public Vector2 range = new Vector2 (5f, 3f);

	Quaternion mStart;
	Vector2 mRot = Vector2.zero;

	private float halfWidth;
	private float halfHeight;

	private Vector3 startingScreenPos;

	void Awake () {
		OnEnable ();
	}

	void OnEnable () {
		mStart = transform.localRotation;
		halfWidth = Screen.width * 0.5f;
		halfHeight = Screen.height * 0.5f;
		startingScreenPos = Camera.main.WorldToScreenPoint (transform.position);
		startingScreenPos = new Vector3 (startingScreenPos.x - halfWidth, startingScreenPos.y - halfHeight, 0f);
	}

	void Update () {
		Vector3 pos = Input.mousePosition;

		float x = Mathf.Clamp ((pos.x - startingScreenPos.x - halfWidth) / halfWidth, -1f, 1f);
		float y = Mathf.Clamp ((pos.y - startingScreenPos.y - halfHeight) / halfHeight, -1f, 1f);
		mRot = Vector2.Lerp (mRot, new Vector2 (x, y), Time.deltaTime * 5f);

		transform.localRotation = mStart * Quaternion.Euler (-mRot.y * range.y, mRot.x * range.x, 0f);
	}
}
