using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;
using UnityEngine.InputSystem.EnhancedTouch;
using static UnityEngine.Rendering.VolumeComponent;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float gravity = 10.0f;
    [SerializeField] private float maxVelocityChange = 10.0f;

    [SerializeField] private float jumpForce;

    [SerializeField] private float gravityMultiplier;

    [SerializeField] private float velocityThreshold;

    private bool isGround = true;
    private float currVelocity = 0f;

    float XIntent = 0;
    float ZIntent = 0;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        XIntent = 0;
        XIntent = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate() {
        if (GameManager.Instance.State == GameManager.GameState.MirrorLevel) {
            XIntent *= -1;
        }

        rb.AddForce(Vector3.down * jumpForce * gravityMultiplier, ForceMode.Acceleration);
        currVelocity = rb.velocity.x;

        Vector3 targetVelocity = new Vector3(XIntent, 0, 0);

        targetVelocity *= speed;

        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);

        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = 0;
        velocityChange.y = 0;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            Jump();

    }

    void Jump() {
        // print("Trying to jump");
        if (!isGround) return;
       //  print("Sucessfully jumped");
        isGround = false;
        //rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);

    }

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.tag == "Moving" || collision.collider.tag == "Stationary" || collision.collider.tag == "Ground") {
            isGround = true;
            // Debug.Log("Grounded");
        } else {
            isGround = false;

            // Debug.Log("Not Grounded!");
        }
    }

    public void Reset() {
        rb.Sleep();
    }

}
