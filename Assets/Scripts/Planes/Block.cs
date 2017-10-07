using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// parent is game plane

public class Block : MonoBehaviour {

	[SerializeField]
	private PlaneState m_currentState;
	public PlaneState state {
		get {
			return m_currentState;
		}
		set {
			transform.SetLocalPosition (null, null, 0f);
			StopAllCoroutines ();
			coloring = false;
			switch (value) {
				case PlaneState.Primed:
					gameObject.SetActive (true);
					m_solidCollider.enabled = false;
					m_triggerCollider.enabled = true;
					m_meshRenderer.material = Game.staticRef.planeManager.primedMaterial;
					m_currentState = value;
					transform.localScale = Vector3.one * 0.8f;
					break;
				case PlaneState.Active:
					StartCoroutine (SlideIntoPlace ());
					m_solidCollider.enabled = true;
					m_triggerCollider.enabled = false;
					m_meshRenderer.material = Game.staticRef.planeManager.activeMaterial;
					m_currentState = value;
					transform.localScale = Vector3.one;
					break;
				case PlaneState.Shelved:
					gameObject.SetActive (false);
					m_currentState = value;
					break;
			}
		}
	}

	[SerializeField]
	private BoxCollider2D m_solidCollider;

	[SerializeField]
	private BoxCollider2D m_triggerCollider;

	[SerializeField]
	private MeshRenderer m_meshRenderer;

	private PlaneSegment m_planeSegment;
	/// <summary>
	/// The plane segment this block is a part of.
	/// </summary>
	public PlaneSegment planeSegment {
		get { return m_planeSegment; }
	}

	void Start () {
		m_planeSegment = transform.parent.GetComponent<PlaneSegment> ();
		m_planeSegment.allBlocks.Push (this);
		if (m_planeSegment.plane == Game.staticRef.planeManager.activePlane) {
			m_currentState = PlaneState.Active;
		}
		else if (m_planeSegment.plane == Game.staticRef.planeManager.primedPlane) {
			m_currentState = PlaneState.Primed;
		}
		else {
			m_currentState = PlaneState.Shelved;
		}
		state = m_currentState;
	}

	private IEnumerator SlideIntoPlace () {
		float timeElapsed = 0f;
		transform.SetLocalPosition (null, null, 1f);
		timeElapsed += Time.deltaTime;
		yield return null;

		while (timeElapsed <= 0.25f) {
			transform.SetLocalPosition (null, null, Mathf.Sin (50f * timeElapsed) / (50f * timeElapsed));
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		transform.SetLocalPosition (null, null, 0f);
	}

	public void StartAnimatingColor () {
		if (!coloring) {
			coloring = true;
			StartCoroutine (StartAnimatingColorHelper ());
		}
	}

	private bool coloring = false;
	private IEnumerator StartAnimatingColorHelper () {
		Renderer rend = GetComponent<Renderer> ();
		float timeElapsed = 0f;
		float duration = 0.5f;
		while (timeElapsed < duration) {
			timeElapsed += Time.deltaTime;
			rend.material.SetColor ("_EmissionColor", rend.material.color * Interpolation.Interpolate (0.25f, 1f, timeElapsed / duration, InterpolationMethod.SquareRoot));
			yield return null;
		}
	}

}
