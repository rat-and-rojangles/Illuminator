using UnityEngine;
using System.Collections;
using Prime31;


public class PlayerCharacter : MonoBehaviour {
	/// <summary>
	/// This much subtracted from velocity per second.
	/// </summary>
	[SerializeField]
	private float gravity = -56.25f;
	[SerializeField]
	private float runSpeed = 8f;

	public float jumpHeight = 3.5f;
	[SerializeField]
	private float fallSpeedCutoff = -26.25f;

	private CharacterController2D controller;

	[SerializeField]
	private Animator animator;
	private Vector2 m_velocity = Vector2.zero;
	public Vector2 velocity {
		get { return m_velocity; }
	}

	[SerializeField]
	private ParticleSystem m_stepParticle;

	[SerializeField]
	private ParticleSystem m_deathParticle;

	private IPlayerInputQuery playerInputQuery;

	private Rigidbody [] ragdollBodies;

	/// <summary>
	/// Derived vertical velocity from jumping
	/// </summary>
	/// <returns></returns>
	private float jumpVelocity {
		get { return Mathf.Sqrt (2f * jumpHeight * -gravity); }
	}

	void Start () {
		controller = GetComponent<CharacterController2D> ();
		ragdollBodies = GetComponentsInChildren<Rigidbody> ();

#if UNITY_EDITOR
		playerInputQuery = new PlayerInputQueryButtons ();
#else
		playerInputQuery = new PlayerInputQueryMobile ();
#endif

		var m = m_stepParticle.main;
		controller.onControllerCollidedEvent += OnControllerCollide;
		m_velocity = Vector2.zero;
	}


	Collider2D lastCollided = null;
	void OnControllerCollide (RaycastHit2D hit) {
		if (hit.collider != lastCollided) {
			lastCollided = hit.collider;
			Block other = hit.collider.transform.parent.GetComponent<Block> ();
			if (other != null) {
				other.Illuminate ();
				var m = m_stepParticle.main;
				m.startColor = Game.staticRef.palette.activeBlockColor;
				m_stepParticle.Play ();
			}
		}
	}

	private bool jumpPressedMidair = false;
	void Update () {
		PlayerInputStruct inputStruct = playerInputQuery.NextInput ();
		if (controller.isGrounded) {
			m_velocity.y = 0;
		}

		jumpPressedMidair = jumpPressedMidair || inputStruct.jumpDown;

		if (controller.isGrounded && (inputStruct.jumpDown || (jumpPressedMidair && inputStruct.jumpHeld))) {
			jumpPressedMidair = false;
			m_velocity.y = jumpVelocity;
			SoundCatalog.staticRef.PlayJumpSound ();
		}
		// short hop
		else if (!inputStruct.jumpHeld && !controller.isGrounded) {
			if (m_velocity.y > jumpVelocity * 0.25f) {
				m_velocity.y = jumpVelocity * 0.25f;
			}
		}

		m_velocity.x = Game.staticRef.autoScroller.scrollSpeed;
		if (controller.isGrounded && transform.position.x < Game.staticRef.boundaries.playerIdealXPosition) {
			m_velocity.x += Mathf.Lerp (runSpeed * 1.75f, runSpeed * 0f, (transform.position.x - Game.staticRef.boundaries.deathLineX) / (Game.staticRef.boundaries.playerIdealXPosition - Game.staticRef.boundaries.deathLineX));
		}

		// apply gravity before moving
		m_velocity.y = Mathf.Clamp (m_velocity.y + gravity * Time.deltaTime, fallSpeedCutoff, Mathf.Infinity);

		controller.move (m_velocity * Time.deltaTime);
		AnimationUpdate ();

		// grab our current velocity to use as a base for all calculations
		m_velocity = controller.velocity;

		if (transform.position.x < Game.staticRef.boundaries.deathLineX || transform.position.y < Game.staticRef.boundaries.deathLineY) {
			Die (false);
		}

		if (inputStruct.swapDown && !Game.staticRef.pauseScreen.isPaused) {
			Game.staticRef.planeManager.Swap ();
		}
	}

	private void AnimationUpdate () {
		if (!controller.isGrounded) {
			animator.Play ("Fall");
		}
		else {
			animator.Play ("Run");
		}
	}

	[SerializeField]
	private PlayerHurtbox hurtbox;

	/// <summary>
	/// Is the character smashed by the wall?
	/// </summary>
	public bool SlamCheck () {
		return hurtbox.SlamCheck ();
	}

	public void Die (bool blastOff) {
		Game.staticRef.pauseScreen.allowPause = false;
		Game.staticRef.scoreCounter.WriteScores ();

		SoundCatalog.staticRef.PlayDeathSound ();
		Game.staticRef.scoreCounter.continueUpdating = false;
		this.enabled = false;
		animator.enabled = false;
		controller.rigidBody2D.simulated = false;
		controller.boxCollider2D.isTrigger = true;
		hurtbox.gameObject.SetActive (false);
		controller.enabled = false;
		animator.transform.parent = null;

		Game.staticRef.camShake.FinalShake ();

		m_deathParticle.Play ();
		Game.staticRef.blockPool.Explode ();
		if (blastOff) {
			foreach (Rigidbody rb in ragdollBodies) {
				rb.isKinematic = false;
				rb.velocity = new Vector3 (Game.staticRef.autoScroller.scrollSpeed + m_velocity.x / 2f, Random.Range (5f, 15f), Random.Range (-10f, -5f));
			}
		}
		else {
			foreach (Rigidbody rb in ragdollBodies) {
				rb.isKinematic = false;
			}
		}
		StartCoroutine (Game.staticRef.Halt ());
	}

	/// <summary>
	/// Solely for configuring the ragdoll in the editor.
	/// </summary>
	[ContextMenu ("EnableKinematic")]
	private void EnableKinematic () {
		foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody> ()) {
			rb.isKinematic = true;
		}
	}
}
