using UnityEngine;
using System.Collections;
using Prime31;


public class PlayerCharacter : MonoBehaviour {
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float jumpHeight = 3f;

	private CharacterController2D controller;

	[SerializeField]
	private Animator animator;
	private Vector3 velocity;

	[SerializeField]
	private PlayerHurtbox hurtbox;

	private Rigidbody [] ragdollBodies;

	void Awake () {
		controller = GetComponent<CharacterController2D> ();
		ragdollBodies = GetComponentsInChildren<Rigidbody> ();
	}


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update () {
		if (Input.GetButtonDown ("Swap")) {
			Game.staticRef.planeManager.Swap ();
		}

		if (controller.isGrounded) {
			velocity.y = 0;
		}

		float horizontalInput = Input.GetAxis ("Horizontal");
		if (horizontalInput > 0.1f) {
			transform.rotation = Quaternion.Euler (Vector3.zero);
			velocity.x = runSpeed + Game.staticRef.AUTO_SCROLL_RATE;
		}
		else if (horizontalInput < -0.1f) {
			transform.rotation = Quaternion.Euler (0f, 180f, 0f);
			velocity.x = -runSpeed;
		}
		else {
			velocity.x = 0f;
		}

		// we can only jump from the ground
		// note the kinematic formula
		if (controller.isGrounded && Input.GetAxis ("Vertical") > 0.1f) {
			velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
			animator.SetTrigger (Animator.StringToHash ("Jump"));
		}

		animator.SetFloat (Animator.StringToHash ("Speed"), Mathf.Abs (velocity.x));
		animator.SetBool (Animator.StringToHash ("Grounded"), controller.isGrounded);

		// apply gravity before moving
		velocity.y += gravity * Time.deltaTime;

		controller.move (velocity * Time.deltaTime);

		// grab our current _velocity to use as a base for all calculations
		velocity = controller.velocity;
	}

	/// <summary>
	/// Would the character be dead when this is called?
	/// </summary>
	public bool DeathCheck () {
		return hurtbox.withinTrigger;
	}

	public void Die () {
		animator.enabled = false;
		controller.enabled = false;
		this.enabled = false;

		foreach (Rigidbody rb in ragdollBodies) {
			rb.isKinematic = false;
			rb.velocity = new Vector3 (0f, -1f, Random.Range (-10, -5));
		}
	}

	[ContextMenu ("EnableKinematic")]
	private void EnableKinematic () {
		foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody> ()) {
			rb.isKinematic = true;
		}
	}
}
