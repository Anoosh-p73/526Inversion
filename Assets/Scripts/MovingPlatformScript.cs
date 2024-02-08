using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{

    Vector3 defaultPos;
    public float speed = 3.0f;  
    public float amplitude = 2.0f; 

    private float timeCounter = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*float pingpong = Mathf.PingPong(Time.time, 2);
        transform.position = new Vector3(defaultPos.x + pingpong, defaultPos.y, defaultPos.z);*/
        timeCounter += Time.deltaTime * speed;
        float xPosition = Mathf.Lerp(-amplitude, amplitude, Mathf.SmoothStep(0.0f, 1.0f, Mathf.Sin(timeCounter)));

        transform.position = new Vector3(defaultPos.x + xPosition, transform.position.y, transform.position.z);

    }
}
