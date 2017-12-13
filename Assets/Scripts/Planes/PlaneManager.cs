using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour {

	private float m_rightEdge = 0f;
	public float rightEdge { get { return m_rightEdge; } }

	/// <summary>
	/// Move the right edge forward by one column.
	/// </summary>
	public void NextColumn () {
		m_rightEdge++;
	}

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
	private Material m_illuminatedMaterial;
	public Material illuminatedMaterial {
		get { return m_illuminatedMaterial; }
	}

	[SerializeField]
	private Material m_primedMaterialSecondary;
	[SerializeField]
	private Material m_slamWarningMaterial;
	public Material slamWarningMaterial {
		get { return m_slamWarningMaterial; }
	}

	[SerializeField]
	private Material m_inverseIlluminatedMaterial;

	private Plane m_planeA;
	public Plane planeA {
		get { return m_planeA; }
	}
	private Plane m_planeB;
	public Plane planeB {
		get { return m_planeB; }
	}

	[SerializeField]
	private Material characterMaterial;

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

	void Start () {
		m_rightEdge = Game.staticRef.boundaries.deathLineX;
		m_planeA = new Plane ();
		m_planeB = new Plane ();
		m_planeA.RegisterOtherPlane (m_planeB);
		m_planeB.RegisterOtherPlane (m_planeA);
		UpdateColors ();
		characterMaterial.color = Game.staticRef.palette.playerColor;
	}

	private void UpdateColors () {
		backgroundCamera.backgroundColor = Game.staticRef.palette.backgroundColor;

		m_illuminatedMaterial.color = Game.staticRef.palette.illuminatedBlockColor;
		m_inverseIlluminatedMaterial.color = Game.staticRef.palette.inverseIlluminatedBlockColor;

		m_activeMaterial.color = Game.staticRef.palette.activeBlockColor;
		m_primedMaterial.color = Game.staticRef.palette.primedBlockColor;
		m_primedMaterialSecondary.color = Game.staticRef.palette.primedBlockColor;
		m_slamWarningMaterial.color = Game.staticRef.palette.slamWarningColor;
	}


	/// <summary>
	/// Swaps the primed and active worlds.
	/// </summary>
	public void Swap () {
		Game.staticRef.camShake.Shake ();
		SoundCatalog.staticRef.PlaySwapSound ();

		m_planeAIsActive = !m_planeAIsActive;

		UpdateColors ();

		activePlane.ApplyState ();
		primedPlane.ApplyState ();

		// do a death check on player
		if (Game.staticRef.player != null && Game.staticRef.player.SlamCheck ()) {
			Game.staticRef.player.Die (true);
		}
	}

#if UNITY_EDITOR
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			RerollPalette ();
		}
	}
	[ContextMenu ("Reroll Palette")]
	private void RerollPalette () {
		Game.staticRef.SetPaletteDebug (new ProceduralPalette (Random.value.Normalized01 (), Random.Range (1f / 8f, 0.25f)));
		UpdateColors ();
		characterMaterial.color = Game.staticRef.palette.playerColor;
	}
#endif
}
