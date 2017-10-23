using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// parent is game plane

public class Block : MonoBehaviour {

	// [SerializeField]
	// private SpriteRenderer m_spriteRenderer;

	[SerializeField]
	/// <summary>
	/// Visualizer for the block when active.
	/// </summary>
	private GameObject m_activeObject;
	[SerializeField]
	/// <summary>
	/// Visualizer for the block when primed.
	/// </summary>
	private GameObject m_primedObject;

	[SerializeField]
	private Renderer m_activeRenderer;
	[SerializeField]
	private Renderer m_primedRenderer;

	[SerializeField]
	private BoxCollider2D m_solidCollider;

	[SerializeField]
	private BoxCollider2D m_triggerCollider;

	[SerializeField]
	private ConstantRotation m_constantRotation;

	private PlaneSegment m_planeSegment;
	/// <summary>
	/// The plane segment this block is a part of.
	/// </summary>
	public PlaneSegment planeSegment {
		get { return m_planeSegment; }
	}

	[SerializeField]
	private PlaneState m_currentState;
	public PlaneState state {
		get {
			return m_currentState;
		}
		set {
			// transform.SetLocalPosition (null, null, 0f);
			StopAllCoroutinesAndReset ();
			switch (value) {
				case PlaneState.Primed:
					m_activeObject.SetActive (false);
					m_primedObject.SetActive (true);
					m_solidCollider.enabled = false;
					m_triggerCollider.enabled = true;
					m_currentState = value;
					break;
				case PlaneState.Active:
					m_constantRotation.enabled = false;
					m_activeObject.SetActive (true);
					m_primedObject.SetActive (false);
					StartCoroutine (SlideIntoPlace ());
					m_solidCollider.enabled = true;
					m_triggerCollider.enabled = false;
					m_currentState = value;
					break;
				case PlaneState.Shelved:
					gameObject.SetActive (false);
					m_currentState = value;
					break;
			}
		}
	}

	/// <summary>
	/// Stops all coroutines and resets all info related to the execution of coroutines.
	/// </summary>
	private void StopAllCoroutinesAndReset () {
		StopAllCoroutines ();
		if (coloring) {
			coloring = false;
			m_activeRenderer.material = Game.staticRef.planeManager.activeMaterial;
		}
		if (m_constantRotation.enabled) {
			m_constantRotation.enabled = false;
			m_primedRenderer.material = Game.staticRef.planeManager.primedMaterial;
		}
	}

	void Start () {
		m_planeSegment = transform.parent.GetComponent<PlaneSegment> ();
		m_planeSegment.allBlocks.Push (this);
		state = m_planeSegment.plane.state;
		if (state == PlaneState.Primed) {
			//StartCoroutine (SpinOnSpawn ());
		}
	}

	private IEnumerator SpinOnSpawn () {
		float timeElapsed = 0f;
		m_constantRotation.enabled = true;
		while (timeElapsed <= BlockProxy.spawnDuration) {
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		m_constantRotation.enabled = false;
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
		m_activeObject.transform.localScale = Vector3.one;
		float duration = 0.1f;

		float timeElapsed = 0f;
		timeElapsed += Time.deltaTime;
		yield return null;

		while (timeElapsed <= duration) {
			float ratio = timeElapsed / duration;
			m_activeObject.transform.localScale = Vector3.one * Interpolation.Interpolate (0f, 1f, ratio, InterpolationMethod.Quadratic);
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		m_activeObject.transform.localScale = Vector3.one;
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
		m_primedRenderer.material = Game.staticRef.planeManager.slamWarningMaterial;
		m_constantRotation.enabled = true;
	}

	/// <summary>
	/// Called when a specific child has a trigger exit event
	/// </summary>
	public void ChildTriggerExit () {
		m_primedRenderer.material = Game.staticRef.planeManager.primedMaterial;
		m_constantRotation.enabled = false;
	}

	private bool coloring = false;
	private IEnumerator StartAnimatingColorHelper () {
		float timeElapsed = 0f;
		float duration = 0.5f;
		while (timeElapsed < duration) {
			timeElapsed += Time.deltaTime;
			m_activeRenderer.material.color = Interpolation.Interpolate (Game.staticRef.palette.activeBlockColor, Game.staticRef.palette.illuminatedBlockColor, timeElapsed / duration, InterpolationMethod.SquareRoot);
			yield return null;
		}
	}

}
