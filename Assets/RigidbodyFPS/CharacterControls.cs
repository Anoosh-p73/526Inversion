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


	private Rigidbody rb;
	private bool grounded = false;

	float XIntent = 0;
	float ZIntent = 0;

	void Update ()
	{
		XIntent = 0;
		ZIntent = 0;

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

			// scale desired movement to our speed
			targetVelocity *= speed;

			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rb.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp (velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp (velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;

			rb.AddForce (velocityChange, ForceMode.VelocityChange);

		}
 
		// We apply gravity manually for more tuning control
		rb.AddForce (new Vector3 (0, -gravity * rb.mass, 0));
 
		grounded = false;
	}

    private void OnCollisionStay(Collision collision) {
		print("grounded");
        grounded = true;
    }

}
