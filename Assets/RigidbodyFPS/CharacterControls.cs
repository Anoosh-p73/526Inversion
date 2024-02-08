// Originally obtained from Unity Wiki:
// https://wiki.unity3d.com/index.php/RigidbodyFPSWalker
//
// Hacked up a bit by @kurtdekker to support more feechurz
//

using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]
 
public class CharacterControls : MonoBehaviour
{
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;

	public bool AllowAirControl = true;

	[Header( "Jumping:")]
	public bool AllowJumping = true;
	public float JumpHeight = 4.0f;

	[Header ("Used to make controls 'local' to heading direction.")]
	public Transform LookTransform;

	private Rigidbody rb;
	private bool grounded = false;

	void Awake ()
	{
		rb = GetComponent<Rigidbody> ();
		rb.freezeRotation = true;
		rb.useGravity = false;
	}

	float XIntent = 0;
	float ZIntent = 0;
	bool JumpIntent = false;

	void Update ()
	{
		XIntent = 0;
		ZIntent = 0;
		JumpIntent = false;

		// gather input intent:

		if (grounded && AllowJumping)
		{
			JumpIntent = Input.GetButtonDown ("Jump");
		}

		if (grounded || AllowAirControl)
		{
			XIntent = Input.GetAxis ("Horizontal");
			// ZIntent = Input.GetAxis ("Vertical");
		}
	}

	void FixedUpdate()
	{
		if (grounded || AllowAirControl)
		{
			// Calculate how fast we want to be moving, normalized
			Vector3 targetVelocity = new Vector3 (XIntent, 0, ZIntent);

			if (LookTransform)
			{
				Quaternion rotateControlsToLocal = Quaternion.Euler (0, LookTransform.eulerAngles.y, 0);
				targetVelocity = rotateControlsToLocal * targetVelocity;
			}

			// scale desired movement to our speed
			targetVelocity *= speed;

			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rb.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp (velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp (velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;

			rb.AddForce (velocityChange, ForceMode.VelocityChange);

			/*if (JumpIntent)
			{
				print("JumpIntent is true");
				velocity = rb.velocity;
				velocity.y = CalculateJumpVerticalSpeed ();
				rb.velocity = velocity;
			}*/
		}
 
		// We apply gravity manually for more tuning control
		rb.AddForce (new Vector3 (0, -gravity * rb.mass, 0));
 
		grounded = false;
	}

    private void OnCollisionStay(Collision collision) {
		print("grounded");
        grounded = true;
    }

    float CalculateJumpVerticalSpeed ()
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt (2 * JumpHeight * gravity);
	}
}
