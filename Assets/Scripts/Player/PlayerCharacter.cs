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
	[SerializeField]
	private float jumpHeight = 3.5f;
	[SerializeField]
	private float fallSpeedCutoff = -26.25f;
	/// <summary>
	/// The character walks for this long before reaching starting to run. (1/10 of normal run speed)
	/// </summary>
	[SerializeField]
	private float runAccelerationTime = 0.15f;

	private float timeUntilFullRunSpeed = 0f;

	private CharacterController2D controller;

	[SerializeField]
	private Animator animator;
	private Vector3 velocity = Vector2.zero;

	[SerializeField]
	private ParticleSystem m_stepParticle;

	[SerializeField]
	private ParticleSystem m_deathParticle;

	private bool swapThisFrame = false;
	private bool jumpThisFrame = false;
	private bool cancelJumpThisFrame = false;

	private Rigidbody [] ragdollBodies;

	[SerializeField]
	private Material characterMaterial;

	/// <summary>
	/// Derived vertical velocity from jumping
	/// </summary>
	/// <returns></returns>
	private float jumpVelocity {
		get { return Mathf.Sqrt (2f * jumpHeight * -gravity); }
	}

	void Start () {
		characterMaterial.color = Game.staticRef.palette.playerColor;
		controller = GetComponent<CharacterController2D> ();
		ragdollBodies = GetComponentsInChildren<Rigidbody> ();
		timeUntilFullRunSpeed = runAccelerationTime;

		var m = m_stepParticle.main;
		m.customSimulationSpace = Game.staticRef.worldTransform;
		controller.onControllerCollidedEvent += OnControllerCollide;
	}

	private Collider2D lastCollided = null;
	void OnControllerCollide (RaycastHit2D hit) {
		if (hit.collider != lastCollided) {
			lastCollided = hit.collider;
			Block other = hit.collider.transform.parent.GetComponent<Block> ();
			if (other != null) {
				other.StartAnimatingColor ();
				var m = m_stepParticle.main;
				m.startColor = Game.staticRef.palette.activeBlockColor;
				m_stepParticle.Play ();
				SoundCatalog.staticRef.PlayRandomFootstepSound ();
			}
		}
	}

	void Update () {
		swapThisFrame = swapThisFrame || Input.GetButtonDown ("Swap");
		jumpThisFrame = jumpThisFrame || Input.GetButtonDown ("Jump");
		jumpThisFrame = jumpThisFrame && !Input.GetButtonUp ("Jump");

		cancelJumpThisFrame = cancelJumpThisFrame || Input.GetButtonUp ("Jump");
	}


	void FixedUpdate () {
		if (controller.isGrounded) {
			velocity.y = 0;
		}

		// we can only jump from the ground
		if (controller.isGrounded && jumpThisFrame) {
			jumpThisFrame = false;
			velocity.y = jumpVelocity;
			timeUntilFullRunSpeed = -1f;
			cancelJumpThisFrame = false;
			SoundCatalog.staticRef.PlayJumpSound ();
		}
		// short hop
		else if (cancelJumpThisFrame && !controller.isGrounded) {
			cancelJumpThisFrame = false;
			if (velocity.y > jumpVelocity * 0.5f) {
				velocity.y = jumpVelocity * 0.5f;
			}
		}

		float horizontalInput = Input.GetAxis ("Horizontal");
		float derivedRunSpeed = horizontalInput * runSpeed;


		if (controller.isGrounded && horizontalInput.Sign () != velocity.x.Sign ()) {
			timeUntilFullRunSpeed = runAccelerationTime;
		}
		else if (!controller.isGrounded) {
			timeUntilFullRunSpeed = -1f;
		}
		if (timeUntilFullRunSpeed > 0f) {
			derivedRunSpeed *= 0.1f;
			timeUntilFullRunSpeed -= Time.fixedDeltaTime;
		}

		if (horizontalInput > 0.1f) {
			transform.rotation = Quaternion.Euler (Vector3.zero);
			velocity.x = derivedRunSpeed + Game.staticRef.AUTO_SCROLL_RATE;
		}
		else if (horizontalInput < -0.1f) {
			transform.rotation = Quaternion.Euler (0f, 180f, 0f);
			velocity.x = derivedRunSpeed;
		}
		else {
			timeUntilFullRunSpeed = runAccelerationTime;
			velocity.x = 0f;
		}

		// apply gravity before moving
		//velocity.y += gravity * Time.fixedDeltaTime;
		velocity.y = Mathf.Clamp (velocity.y + gravity * Time.fixedDeltaTime, fallSpeedCutoff, Mathf.Infinity);

		controller.move (velocity * Time.fixedDeltaTime);
		AnimationUpdate ();

		// grab our current velocity to use as a base for all calculations
		velocity = controller.velocity;

		if (transform.position.x < Game.staticRef.leftBoundary || transform.position.y < Game.staticRef.bottomBoundary) {
			DieFromFall ();
		}

		if (swapThisFrame) {
			Game.staticRef.planeManager.Swap ();
			swapThisFrame = false;
		}
	}

	private void AnimationUpdate () {
		if (!controller.isGrounded) {
			animator.Play ("Fall");
		}
		else if (velocity.x.Sign () == 0) {
			animator.Play ("Idle");
		}
		else {
			if (timeUntilFullRunSpeed > 0f) {
				animator.Play ("Idle");
			}
			else {
				animator.Play ("Run");
			}
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


	public void DieFromSlam () {
		SoundCatalog.staticRef.PlayDeathSound ();
		Game.staticRef.scoreCounter.continueUpdating = false;
		animator.enabled = false;
		controller.enabled = false;
		this.enabled = false;

		m_deathParticle.Play ();

		foreach (Rigidbody rb in ragdollBodies) {
			rb.isKinematic = false;
			rb.velocity = new Vector3 (Game.staticRef.AUTO_SCROLL_RATE + velocity.x / 2f, Random.Range (5f, 15f), Random.Range (-10f, -5f));
		}
		StartCoroutine (Game.staticRef.Halt ());
	}
	public void DieFromFall () {
		SoundCatalog.staticRef.PlayDeathSound ();
		Game.staticRef.scoreCounter.continueUpdating = false;
		animator.enabled = false;
		controller.enabled = false;
		this.enabled = false;

		m_deathParticle.Play ();

		foreach (Rigidbody rb in ragdollBodies) {
			rb.isKinematic = false;
			//rb.velocity = velocity * 0.25f;
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
