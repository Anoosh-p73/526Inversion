using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{

    Vector3 defaultPos;
    public float speed = 3.0f;  
    public float amplitude = 2.0f;

    private GameObject target = null;
    private Vector3 offset;

    private float timeCounter = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        float pingpong = Mathf.PingPong(Time.time * 5, 4);
        transform.position = new Vector3(defaultPos.x + pingpong, defaultPos.y, defaultPos.z);
        /*timeCounter += Time.deltaTime * speed;
        float xPosition = Mathf.Lerp(-amplitude, amplitude, Mathf.SmoothStep(0.0f, 1.0f, Mathf.Sin(timeCounter)));

        transform.position = new Vector3(defaultPos.x + xPosition, transform.position.y, transform.position.z);*/

    }


    void OnTriggerStay(Collider col) {
        target = col.gameObject;
        offset = target.transform.position - transform.position;
    }
    void OnTriggerExit(Collider col) {
        target = null;
    }

    void LateUpdate() {
        if (target != null) {
            target.transform.position = transform.position + offset;
        }

    }

    /*    private void OnCollisionStay(Collision collision) {
            if (collision.collider.tag == "Player") {
                print($"Collided: {collision.gameObject.name}");
                transform.parent = collision.transform;

            }
        }*/

    /*    function OnTriggerStay(other:Collider) {

            if (other.gameObject.tag == "platform") {
                transform.parent = other.transform;

            }
        }*/


}
