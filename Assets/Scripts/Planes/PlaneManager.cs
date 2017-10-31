using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour {

	[SerializeField]
	private Material m_activeMaterial;
	public Material activeMaterial {
		get { return m_activeMaterial; }
	}
	[SerializeField]
	private Material m_primedMaterial;
	public Material primedMaterial {
		get { return m_primedMaterial; }
	}
	[SerializeField]
	private Material m_primedMaterialSecondary;
	[SerializeField]
	private Material m_slamWarningMaterial;
	public Material slamWarningMaterial {
		get { return m_slamWarningMaterial; }
	}

	private Plane m_planeA;
	public Plane planeA {
		get { return m_planeA; }
	}
	private Plane m_planeB;
	public Plane planeB {
		get { return m_planeB; }
	}

	[SerializeField]
	/// <summary>
	/// The camera that renders the background color.
	/// </summary>
	private Camera backgroundCamera;


	private bool m_planeAIsActive;
	/// <summary>
	/// True if plane A is active, false if plane B is active.
	/// </summary>
	public bool planeAIsActive {
		get { return m_planeAIsActive; }
	}


	public Plane activePlane {
		get { return m_planeAIsActive ? m_planeA : m_planeB; }
	}
	public Plane primedPlane {
		get { return m_planeAIsActive ? m_planeB : m_planeA; }
	}

	[SerializeField]
	private PlaneSegment initialSegmentA;
	[SerializeField]
	private PlaneSegment initialSegmentB;

	void Awake () {
		m_planeA = new Plane ();
		m_planeA.PushSegment (initialSegmentA);
		m_planeB = new Plane ();
		m_planeB.PushSegment (initialSegmentB);
	}

	void Start () {
		UpdateColors ();
	}

	private void UpdateColors () {
		backgroundCamera.backgroundColor = Game.staticRef.palette.backgroundColor;
		m_activeMaterial.color = Game.staticRef.palette.activeBlockColor;
		m_primedMaterial.color = Game.staticRef.palette.primedBlockColor;
		m_primedMaterialSecondary.color = Game.staticRef.palette.primedBlockColor;
		m_slamWarningMaterial.color = Game.staticRef.palette.slamWarningColor;
	}

	/// <summary>
	/// Swaps the primed and active worlds.
	/// </summary>
	public void Swap () {
		SoundCatalog.staticRef.PlaySwapSound ();

		m_planeAIsActive = !m_planeAIsActive;

		UpdateColors ();

		activePlane.ApplyState ();
		primedPlane.ApplyState ();

		// do a death check on player
		if (Game.staticRef.player != null && Game.staticRef.player.SlamCheck ()) {
			Game.staticRef.player.DieFromSlam ();
		}
	}
}
