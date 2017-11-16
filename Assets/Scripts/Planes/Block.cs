using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

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

	private bool shelved = false;

	private Rigidbody2D rb2;
	[SerializeField]
	private BoxCollider boxCollider;

	private Plane m_plane;
	/// <summary>
	/// The plane this block is a part of.
	/// </summary>
	public Plane plane {
		get { return m_plane; }
	}

	/// <summary>
	/// Stops all coroutines, restores the block to its normal state.
	/// </summary>
	private void Reset () {
		if (illuminated) {
			StopAllCoroutines ();
			illuminated = false;
			m_activeRenderer.material = Game.staticRef.planeManager.activeMaterial;
		}
		if (m_constantRotation.enabled) {
			m_constantRotation.enabled = false;
			m_primedRenderer.material = Game.staticRef.planeManager.primedMaterial;
		}
	}

	private void SetState (PlaneState value) {
		Reset ();
		switch (value) {
			case PlaneState.Primed:
				shelved = false;
				transform.SetLocalPosition (null, null, -1f);
				m_activeObject.SetActive (false);
				m_primedObject.SetActive (true);
				m_solidCollider.enabled = false;
				m_triggerCollider.enabled = true;
				boxCollider.isTrigger = true;
				break;
			case PlaneState.Active:
				shelved = false;
				transform.SetLocalPosition (null, null, 0f);
				m_activeObject.SetActive (true);
				m_primedObject.SetActive (false);
				StartCoroutine (SlideIntoPlace ());
				m_solidCollider.enabled = true;
				m_triggerCollider.enabled = false;
				boxCollider.isTrigger = false;
				break;
			case PlaneState.Shelved:
				shelved = true;
				m_activeObject.SetActive (false);
				m_primedObject.SetActive (false);
				m_solidCollider.enabled = false;
				m_triggerCollider.enabled = false;
				boxCollider.isTrigger = true;
				break;
		}
	}

	public void SpawnSelf (Vector3 pos, Plane plane) {
		Reset ();
		plane.OnPlaneSwap += SetState;
		m_plane = plane;
		SetState (PlaneState.Shelved);
		transform.position = pos;
		gameObject.SetActive (true);
	}
	private void Despawn () {
		plane.OnPlaneSwap -= SetState;
		gameObject.SetActive (false);
		SetState (PlaneState.Shelved);
	}

	void Awake () {
		rb2 = GetComponent<Rigidbody2D> ();
		SetState (PlaneState.Shelved);
	}

	void Update () {
		if (!shelved && transform.position.x < Game.staticRef.boundaries.deathLineX) {        // if x < despawn
			Despawn ();
		}
		else if (shelved && transform.position.x < Game.staticRef.boundaries.revealLineX) {   // if x < reveal
			SetState (plane.state);
		}
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

	public void Illuminate () {
		if (!illuminated) {
			SoundCatalog.staticRef.PlayRandomFootstepSound ();
			illuminated = true;
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

	private bool illuminated = false;
	private IEnumerator StartAnimatingColorHelper () {
		float timeElapsed = 0f;
		float duration = 0.5f;
		while (timeElapsed < duration) {
			timeElapsed += Time.deltaTime;
			m_activeRenderer.material.color = Interpolation.Interpolate (Game.staticRef.palette.activeBlockColor, Game.staticRef.palette.illuminatedBlockColor, timeElapsed / duration, InterpolationMethod.SquareRoot);
			yield return null;
		}
	}

	public void Explode () {
		// Rigidbody2D rb2 = GetComponent<Rigidbody2D> ();
		// rb2.isKinematic = false;
		rb2.simulated = true;
		rb2.bodyType = RigidbodyType2D.Dynamic;
		rb2.velocity = Vector2.zero;
		rb2.AddForce ((transform.position - Game.staticRef.player.transform.position) * Random.value * 200f);
		boxCollider.enabled = false;
	}

}
