using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	/// <summary>
	/// Use this to instantiate new blocks. Kind of a workaround really
	/// </summary>
	public GameObject BLOCK_PREFAB;

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
	private Transform m_worldTransform;
	/// <summary>
	/// Reference to the transform of the moving world.
	/// </summary>
	public Transform worldTransform {
		get { return m_worldTransform; }
	}

	[SerializeField]
	[Range (0f, 1f)]
	private float screenGravitateFactor = 0.5f;

	private float m_topCamBound;
	private float m_rightCamBound;
	/// <summary>
	/// Despawn boundary for plane segments and death boundary for player.
	/// </summary>
	public float leftBoundary {
		get {
			return -m_rightCamBound - 2f;
		}
	}

	/// <summary>
	/// Abyss for player.
	/// </summary>
	public float bottomBoundary {
		get {
			return -m_topCamBound - 2f;
		}
	}

	/// <summary>
	/// Right boundary where segments are spawned.
	/// </summary>
	public float rightSpawnBoundary {
		get {
			return m_rightCamBound - 1f;
		}
	}

	public float gravitateXPosition {
		get {
			return 2f * m_rightCamBound * screenGravitateFactor - m_rightCamBound;
		}
	}
	/// <summary>
	/// character color
	/// </summary>
	[SerializeField]
	[Range (0f, 1f)]
	private float baseHue;
	private Palette m_palette;
	/// <summary>
	/// Colors of various objects in the game.
	/// </summary>
	/// <returns></returns>
	public Palette palette {
		get { return m_palette; }
	}

	private int difficulty = 0;
	private float [] speedByDifficulty = { 5f, 7.5f, 10f, 12.5f, 15f, 17.5f, 20f, 22.5f, 25f };

	private float difficultyTimeElapsed = 0f;
	[SerializeField]
	private float difficultyChangeTime = 5f;


	[SerializeField]
	private bool startAtZero = false;
	/// <summary>
	/// Rate at which the level scrolls.
	/// </summary>
	public float AUTO_SCROLL_RATE = 2.0f;

	void Awake () {
		// m_palette = new ProceduralPalette (baseHue);
		m_palette = new ProceduralPalette (Random.value.Normalized01 (), Random.Range (1f / 8f, 0.25f));
		m_staticRef = this;
		if (Camera.main.orthographic) {
			m_topCamBound = Camera.main.orthographicSize;
			m_rightCamBound = m_topCamBound * Screen.width / Screen.height;
		}
		else {
			m_topCamBound = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0f, Camera.main.transform.position.z)).y;
			m_rightCamBound = -Camera.main.ViewportToWorldPoint (new Vector3 (1f, 1f, Camera.main.transform.position.z)).x;
		}
	}

	void OnDestroy () {
		m_staticRef = null;
	}

	void Start () {
		if (!startAtZero) {
			AUTO_SCROLL_RATE = speedByDifficulty [0];
		}
		else {
			AUTO_SCROLL_RATE = 0;
		}
		m_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerCharacter> ();
	}

	void Update () {
		if (!startAtZero && difficulty < speedByDifficulty.Length - 1) {
			difficultyTimeElapsed += Time.deltaTime;
			if (difficultyTimeElapsed >= difficultyChangeTime) {
				difficultyTimeElapsed = 0f;
				difficulty++;
				AUTO_SCROLL_RATE = speedByDifficulty [difficulty];
				SoundCatalog.staticRef.PlaySpeedUpSound ();
			}
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
		foreach (PlaneSegment ps in Game.staticRef.planeManager.activePlane.planeSegments) {
			foreach (Block b in ps.allBlocks) {
				// Rigidbody2D temp = b.gameObject.AddComponent<Rigidbody2D> ();
				// temp.AddForce ((b.transform.position - player.transform.position) * Random.value * 100f);
				b.Explode ();
			}
		}
		foreach (PlaneSegment ps in Game.staticRef.planeManager.primedPlane.planeSegments) {
			foreach (Block b in ps.allBlocks) {
				// Rigidbody2D temp = b.gameObject.AddComponent<Rigidbody2D> ();
				// temp.AddForce ((b.transform.position - player.transform.position) * Random.value * 100f);
				b.Explode ();
			}
		}
		difficulty = int.MaxValue;
		MusicMaster.staticRef.HaltMusic (HALT_DURATION, HALT_INTERP_METHOD);
		Transform cam = Camera.main.transform.parent.transform;
		float timeElapsed = 0f;
		float originalScrollRate = AUTO_SCROLL_RATE;
		while (timeElapsed <= HALT_DURATION) {
			timeElapsed += Time.deltaTime;
			float ratio = timeElapsed / HALT_DURATION;
			AUTO_SCROLL_RATE = Interpolation.Interpolate (originalScrollRate, 2.5f, ratio, HALT_INTERP_METHOD);
			yield return null;
		}

		loseScreen.SetActive (true);
#if UNITY_EDITOR
		while (!Input.GetButtonDown ("Swap")) {
			yield return null;
		}
#else
	while (!Utility.ScreenTappedThisFrame ()) {
			yield return null;
		}
#endif
		MusicMaster.staticRef.FadeInMusic (1.25f, HALT_INTERP_METHOD);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

	}
}
