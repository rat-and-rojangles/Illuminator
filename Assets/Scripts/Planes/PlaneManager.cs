using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour {
	private int m_currentActiveIndex = 0;
	public int currentActiveIndex {
		get { return m_currentActiveIndex; }
	}
	public int m_currentPrimedIndex = 1;
	public int currentPrimedIndex {
		get { return m_currentPrimedIndex; }
	}

	[SerializeField]
	private int planesInThisLevel;

	public Plane [] m_planes = null;

	/// <summary>
	/// All planes in the world.
	/// </summary>
	public Plane [] planes {
		get { return m_planes; }
	}

	public Plane activePlane {
		get { return planes [currentActiveIndex]; }
	}
	public Plane primedPlane {
		get { return planes [currentPrimedIndex]; }
	}

	public Material activeMaterial;
	public Material primedMaterial;

	void Awake () {
		m_planes = new Plane [planesInThisLevel];

		float baseHue = PlayerPrefs.GetFloat ("CharacterHue", 0f);
		for (int x = 0; x < planesInThisLevel; x++) {
			planes [x] = new Plane (Color.HSVToRGB (Utility.DecimalPart (baseHue + (x + 1) * 1.0f / (planesInThisLevel + 1f)), 1f, 1f));
		}
	}

	void Start () {
		ApplyColors ();
	}

	/// <summary>
	/// Swaps the primed and active worlds.
	/// </summary>
	public void Swap () {
		SoundCatalog.staticRef.PlaySwapSound ();
		// first do a death check on player
		if (Game.staticRef.player != null && Game.staticRef.player.SlamCheck ()) {
			Game.staticRef.player.DieFromSlam ();
		}

		Utility.Swap (ref m_currentActiveIndex, ref m_currentPrimedIndex);
		ApplyColors ();

		activePlane.ApplyState ();
		primedPlane.ApplyState ();
	}


	public void DespawnOldestSegment (int planeIndex) {
		PlaneSegment rip = Game.staticRef.planeManager.planes [planeIndex].planeSegments.Dequeue ();
		GameObject.Destroy (rip.gameObject, 0.1f);
	}

	private void ApplyColors () {
		activeMaterial.color = planes [currentActiveIndex].color;
		activeMaterial.SetColor ("_EmissionColor", activeMaterial.color * 0.25f);
		primedMaterial.color = planes [currentPrimedIndex].color;
		primedMaterial.SetColor ("_EmissionColor", planes [currentPrimedIndex].color * 0.5f);
	}
}
