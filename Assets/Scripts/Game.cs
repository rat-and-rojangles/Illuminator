using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
	private static Game m_staticRef = null;
	public static Game staticRef {
		get { return m_staticRef; }
	}

	[SerializeField]
	private PlaneManager m_planeManager;

	[SerializeField]
	private GameObject loseScreen;

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


	private static float HALT_DURATION {
		get { return 1.5f; }
	}
	private static InterpolationMethod HALT_INTERP_METHOD {
		get { return InterpolationMethod.SquareRoot; }
	}
	/// <summary>
	/// Gradually halt the level auto scroll.
	/// </summary>
	public IEnumerator Halt () {
		float timeElapsed = 0f;
		float originalScrollRate = AUTO_SCROLL_RATE;
		while (timeElapsed <= HALT_DURATION) {
			timeElapsed += Time.deltaTime;
			float ratio = timeElapsed / HALT_DURATION;
			AUTO_SCROLL_RATE = Interpolation.Interpolate (originalScrollRate, 0f, ratio, HALT_INTERP_METHOD);
			MusicMaster.staticRef.lowPassFilter.cutoffFrequency = Interpolation.Interpolate (22000f, MusicMaster.staticRef.lowPassMinCutoff, ratio, HALT_INTERP_METHOD);
			yield return null;
		}
		loseScreen.SetActive (true);
		while (!Input.GetButton ("Swap")) {
			yield return null;
		}
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
