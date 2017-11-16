using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour {
	[SerializeField]
	[Range (0f, 1f)]
	private float screenGravitateFactor = 0.5f;

	private float m_topCamBound;
	private float m_rightCamBound;
	/// <summary>
	/// Despawn boundary for plane segments and death boundary for player.
	/// </summary>
	public float deathLineX {
		get {
			return transform.position.x - m_rightCamBound - 2f;
		}
	}

	/// <summary>
	/// Abyss for player.
	/// </summary>
	public float deathLineY {
		get {
			return transform.position.y - m_topCamBound - 2f;
		}
	}

	/// <summary>
	/// Right boundary where segments are spawned.
	/// </summary>
	public float spawnLineX {
		get {
			return transform.position.x + m_rightCamBound;
		}
	}
	/// <summary>
	/// Right boundary where segments are revealed.
	/// </summary>
	public float revealLineX {
		get {
			return transform.position.x + m_rightCamBound - 1f;
		}
	}

	public float playerIdealXPosition {
		get {
			return transform.position.x + 2f * m_rightCamBound * screenGravitateFactor - m_rightCamBound;
		}
	}

	void Awake () {
		if (Camera.main.orthographic) {
			m_topCamBound = Camera.main.orthographicSize;
			m_rightCamBound = m_topCamBound * Screen.width / Screen.height;
		}
		else {
			m_topCamBound = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0f, Camera.main.transform.position.z)).y;
			m_rightCamBound = -Camera.main.ViewportToWorldPoint (new Vector3 (1f, 1f, Camera.main.transform.position.z)).x;
		}
	}
}
