using UnityEngine;
using System.Collections;
using Prime31;


public class WoodMan : MonoBehaviour {
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
	private PlayerHurtbox hurtbox;

	private bool swapThisFrame = false;

	private Rigidbody [] ragdollBodies;

	/// <summary>
	/// Derived vertical velocity from jumping
	/// </summary>
	/// <returns></returns>
	private float jumpVelocity {
		get { return Mathf.Sqrt (2f * jumpHeight * -gravity); }
	}

	void Awake () {
		controller = GetComponent<CharacterController2D> ();
		ragdollBodies = GetComponentsInChildren<Rigidbody> ();
		timeUntilFullRunSpeed = runAccelerationTime;
	}

	void Update () {
		swapThisFrame = swapThisFrame || Input.GetButtonDown ("Swap");
	}

	void FixedUpdate () {
		if (controller.isGrounded) {
			velocity.y = 0;
		}

		// we can only jump from the ground
		// note the kinematic formula
		if (controller.isGrounded && Input.GetButton ("Jump")) {
			velocity.y = jumpVelocity;
			//animator.SetTrigger (Animator.StringToHash ("Jump"));
			timeUntilFullRunSpeed = -1f;
		}
		// short hop
		else if (Input.GetButtonUp ("Jump") && !controller.isGrounded) {
			if (velocity.y > jumpVelocity * 0.5f) {
				velocity.y = jumpVelocity * 0.5f;
			}
		}

		float horizontalInput = Input.GetAxis ("Horizontal");
		float derivedRunSpeed = horizontalInput * runSpeed;


		if (controller.isGrounded && horizontalInput.Sign () != velocity.x.Sign ()) {
			timeUntilFullRunSpeed = runAccelerationTime;
		}
		if (timeUntilFullRunSpeed > 0f) {
			derivedRunSpeed *= 0.1f;
			timeUntilFullRunSpeed -= Time.fixedDeltaTime;
		}

		if (horizontalInput > 0.1f) {
			transform.rotation = Quaternion.Euler (Vector3.zero);
			velocity.x = derivedRunSpeed + 0;
		}
		else if (horizontalInput < -0.1f) {
			transform.rotation = Quaternion.Euler (0f, 180f, 0f);
			velocity.x = derivedRunSpeed;
		}
		else {
			timeUntilFullRunSpeed = runAccelerationTime;
			velocity.x = 0f;
		}

		//animator.SetFloat (Animator.StringToHash ("Speed"), Mathf.Abs (velocity.x));
		//animator.SetBool (Animator.StringToHash ("Grounded"), controller.isGrounded);

		// apply gravity before moving
		//velocity.y += gravity * Time.fixedDeltaTime;
		velocity.y = Mathf.Clamp (velocity.y + gravity * Time.fixedDeltaTime, fallSpeedCutoff, Mathf.Infinity);

		controller.move (velocity * Time.fixedDeltaTime);
		AnimationUpdate ();

		// grab our current _velocity to use as a base for all calculations
		velocity = controller.velocity;

		if (transform.position.y < -20f) {
			DieFromFall ();
		}
		else if (transform.position.x < -12f) {
			DieFromFall ();
		}
		if (swapThisFrame) {
			//Game.staticRef.planeManager.Swap ();
			swapThisFrame = false;
		}
	}

	/// <summary>
	/// Would the character be smashed by a wall if you swapped right now?
	/// </summary>
	public bool SlamCheck () {
		//return hurtbox.withinTrigger;
		return false;
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

	public void DieFromSlam () {
		Game.staticRef.scoreCounter.continueUpdating = false;
		animator.enabled = false;
		controller.enabled = false;
		this.enabled = false;

		foreach (Rigidbody rb in ragdollBodies) {
			rb.isKinematic = false;
			rb.velocity = new Vector3 (0f, -1f, Random.Range (-10f, -5f));
		}
		StartCoroutine (Game.staticRef.Halt ());
	}
	public void DieFromFall () {
		Game.staticRef.scoreCounter.continueUpdating = false;
		controller.enabled = false;
		this.enabled = false;
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
