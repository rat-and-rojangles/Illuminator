using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the continuous scrolling in the game.
/// </summary>
public class AutoScroller : MonoBehaviour {
	[SerializeField]
	private float minSpeed;
	[SerializeField]
	private float maxSpeed;

	private float m_scrollSpeed;
	public float scrollSpeed {
		get { return m_scrollSpeed; }
	}

	/// <summary>
	/// Call this when game difficulty changes.
	/// </summary>
	public void SetSpeedRatio (float ratio) {
		m_scrollSpeed = Mathf.Lerp (minSpeed, maxSpeed, ratio);
	}

	/// <summary>
	/// Forcibly set the scroll speed.
	/// </summary>
	public void SetSpeedManual (float speed) {
		m_scrollSpeed = speed;
	}

	void Start () {
		m_scrollSpeed = minSpeed;
	}

	void Update () {
		transform.position += Vector3.right * scrollSpeed * Time.deltaTime;
	}
}
