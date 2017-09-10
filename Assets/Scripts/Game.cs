using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	private static Game m_staticRef = null;
	public static Game staticRef {
		get { return m_staticRef; }
	}

	[SerializeField]
	private PlaneManager m_planeManager;

	public PlaneManager planeManager {
		get { return m_planeManager; }
	}

	[SerializeField]
	private PlayerCharacter m_player;
	public PlayerCharacter player {
		get { return m_player; }
	}


	/// <summary>
	/// Rate at which the level scrolls.
	/// </summary>
	public float AUTO_SCROLL_RATE = 2.0f;

	void Awake () {
		m_staticRef = this;
	}

	void OnDestroy () {
		m_staticRef = null;
	}
}
