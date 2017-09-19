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
			planes [x] = new Plane (Color.HSVToRGB (Utility.DecimalPart (baseHue + (x + 1) * 1.0f / (planesInThisLevel + 1)), 1, 1));
		}
	}

	void Start () {
		ApplyColors ();
	}

	/// <summary>
	/// Swaps the primed and active worlds.
	/// </summary>
	public void Swap () {
		// first do a death check on player
		if (Game.staticRef.player.SlamCheck ()) {
			Game.staticRef.player.DieFromSlam ();
		}

		Utility.Swap (ref m_currentActiveIndex, ref m_currentPrimedIndex);
		ApplyColors ();

		activePlane.ApplyState ();
		primedPlane.ApplyState ();
	}

	private void ApplyColors () {
		activeMaterial.color = planes [currentActiveIndex].color;
		primedMaterial.color = planes [currentPrimedIndex].color;
	}
}
