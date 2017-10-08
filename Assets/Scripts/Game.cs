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

	private PlayerCharacter m_player;
	public PlayerCharacter player {
		get { return m_player; }
	}

	[SerializeField]
	private ScoreCounter m_scoreCounter;
	public ScoreCounter scoreCounter {
		get { return m_scoreCounter; }
	}

	[SerializeField]
	private Transform boundaryObject;
	/// <summary>
	/// Despawn boundary for plane segments and death boundary for player.
	/// </summary>
	public float leftBoundary {
		get {
			return boundaryObject.position.x;
		}
	}

	[SerializeField]
	private Transform m_worldTransform;
	/// <summary>
	/// Reference to the transform of the moving world.
	/// </summary>
	public Transform worldTransform {
		get { return m_worldTransform; }
	}

	/// <summary>
	/// Abyss for player.
	/// </summary>
	public float bottomBoundary {
		get {
			return boundaryObject.position.y;
		}
	}

	/// <summary>
	/// Right boundary where segments are spawned.
	/// </summary>
	public float rightSpawnBoundary {
		get {
			return -boundaryObject.position.x;
		}
	}

	private int difficulty = 0;
	private float [] speedByDifficulty = { 5f, 7.5f, 10f, 12.5f, 15f, 17.5f, 20f, 22.5f, 25f };
	private float [] distanceToNextDifficulty = { 100f, 200f, 400f, 600f, 900f, 1200f, 1600f, 2000f, Mathf.Infinity };


	[SerializeField]
	private bool checkPrefsForSpeed = true;
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

	void Start () {
		// if (checkPrefsForSpeed) {
		// 	AUTO_SCROLL_RATE = PlayerPrefs.GetFloat ("Speed", 8f);
		// }
		AUTO_SCROLL_RATE = speedByDifficulty [0];
		m_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerCharacter> ();
	}

	void Update () {
		if (scoreCounter.score > distanceToNextDifficulty [difficulty]) {
			difficulty++;
			AUTO_SCROLL_RATE = speedByDifficulty [difficulty];
			SoundCatalog.staticRef.PlayDeathSound ();
		}
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
		MusicMaster.staticRef.HaltMusic (HALT_DURATION, HALT_INTERP_METHOD);
		Transform cam = Camera.main.transform.parent.transform;
		float timeElapsed = 0f;
		float originalScrollRate = AUTO_SCROLL_RATE;
		while (timeElapsed <= HALT_DURATION) {
			timeElapsed += Time.deltaTime;
			float ratio = timeElapsed / HALT_DURATION;
			AUTO_SCROLL_RATE = Interpolation.Interpolate (originalScrollRate, 0f, ratio, HALT_INTERP_METHOD);
			float eulerZ = Interpolation.Interpolate (0f, 5f, ratio, HALT_INTERP_METHOD);
			cam.eulerAngles = new Vector3 (0f, 0f, eulerZ);
			yield return null;
		}

		loseScreen.SetActive (true);
		bool waitingToExit = true;
		while (waitingToExit) {
			if (Input.GetButtonDown ("Swap")) {
				waitingToExit = false;
				MusicMaster.staticRef.FadeInMusic (1.25f, HALT_INTERP_METHOD);
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
			else if (Input.GetKeyDown (KeyCode.Backspace)) {
				waitingToExit = false;
				SceneManager.LoadScene (0);
			}
			else {
				yield return null;
			}
		}

	}
}
