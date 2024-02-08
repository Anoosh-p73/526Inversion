using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] private float fullForce;
    [SerializeField] private float continueForce;
    [SerializeField] private float jumpForce;

    [SerializeField] private float gravityMultiplier;

    [SerializeField] private float velocityThreshold;

    private bool isGround = true;
    private float currVelocity = 0f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        rb.AddForce(Vector3.down * jumpForce * gravityMultiplier, ForceMode.Acceleration);
        currVelocity = rb.velocity.x;

        /*if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            MoveForward();
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            MoveBackward();*/
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            Jump();

    }

    void MoveForward() {
        // print("Going RIght");
        // if(currVelocity < 0) 
        if(currVelocity > velocityThreshold) rb.AddForce(Vector3.right * continueForce, ForceMode.Acceleration);
        else rb.AddForce(Vector3.right * fullForce, ForceMode.Acceleration);
    }
    void MoveBackward() {
        // print("Going Left");
        // if (currVelocity > 0) rb.AddForce(Vector3.left * fullForce, ForceMode.Impulse);
        if (currVelocity < -velocityThreshold) rb.AddForce(Vector3.left * continueForce, ForceMode.Impulse);
        else rb.AddForce(Vector3.left * fullForce, ForceMode.Impulse);
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
