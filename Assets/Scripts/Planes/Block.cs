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
					m_meshRenderer.transform.localScale = Vector3.one * 0.8f;
					m_solidCollider.enabled = false;
					m_triggerCollider.enabled = true;
					//m_collider.enabled = false;
					m_meshRenderer.material = Game.staticRef.planeManager.primedMaterial;
					m_currentState = value;
					break;
				case PlaneState.Active:
					//ChildTriggerExit ();
					StartCoroutine (SlideIntoPlace ());
					m_solidCollider.enabled = true;
					m_triggerCollider.enabled = false;
					//m_collider.enabled = true;
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
	private MeshRenderer m_meshRenderer;

	[SerializeField]
	private BoxCollider2D m_solidCollider;

	[SerializeField]
	private BoxCollider2D m_triggerCollider;

	private PlaneSegment m_planeSegment;
	/// <summary>
	/// The plane segment this block is a part of.
	/// </summary>
	public PlaneSegment planeSegment {
		get { return m_planeSegment; }
	}

	void Awake () {
		//m_collider = GetComponent<BoxCollider2D> ();
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

	private IEnumerator SlideIntoPlace2 () {
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

	private IEnumerator SlideIntoPlace () {
		m_meshRenderer.transform.localScale = Vector3.one;
		float duration = 0.1f;

		float timeElapsed = 0f;
		timeElapsed += Time.deltaTime;
		yield return null;

		while (timeElapsed <= duration) {
			float ratio = timeElapsed / duration;
			m_meshRenderer.transform.localScale = Vector3.one * Interpolation.Interpolate (0f, 1f, ratio, InterpolationMethod.Quadratic);
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		m_meshRenderer.transform.localScale = Vector3.one;
	}

	public void StartAnimatingColor () {
		if (!coloring) {
			coloring = true;
			StartCoroutine (StartAnimatingColorHelper ());
		}
	}

	/// <summary>
	/// Called when a specific child has a trigger enter event
	/// </summary>
	public void ChildTriggerEnter () {
		m_meshRenderer.GetComponent<MeshJitter>().enabled = true;
		print ("enter");
		Color myColor = m_meshRenderer.material.color;
		m_meshRenderer.material = Game.staticRef.planeManager.activeMaterial;
		m_meshRenderer.material.color = myColor;
		m_meshRenderer.material.SetColor ("_EmissionColor", myColor);
		m_meshRenderer.material.SetTextureOffset ("_MainTex", Vector2.zero);
	}

	/// <summary>
	/// Called when a specific child has a trigger exit event
	/// </summary>
	public void ChildTriggerExit () {
		m_meshRenderer.GetComponent<MeshJitter>().enabled = false;
		print ("exit");
		m_meshRenderer.material = Game.staticRef.planeManager.primedMaterial;
	}

	private bool coloring = false;
	private IEnumerator StartAnimatingColorHelper () {
		float timeElapsed = 0f;
		float duration = 0.5f;
		m_meshRenderer.material.SetTextureOffset ("_MainTex", Vector2.zero);
		while (timeElapsed < duration) {
			timeElapsed += Time.deltaTime;
			m_meshRenderer.material.SetColor ("_EmissionColor", m_meshRenderer.material.color * Interpolation.Interpolate (0.25f, 1f, timeElapsed / duration, InterpolationMethod.SquareRoot));
			yield return null;
		}
	}

}
