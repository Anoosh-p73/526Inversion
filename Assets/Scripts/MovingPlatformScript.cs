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

    private bool touched = false;

    Transform mirrorLevelTransform;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!touched || GameManager.Instance.State == GameManager.GameState.InitialLevel) {
            float pingpong = Mathf.PingPong(Time.time * 5, 4);
            transform.position = new Vector3(defaultPos.x + pingpong, defaultPos.y, defaultPos.z);
        } else if (GameManager.Instance.State == GameManager.GameState.MirrorLevel && touched) {
            transform.position = mirrorLevelTransform.position;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (GameManager.Instance.State != GameManager.GameState.MirrorLevel){
            touched = true;
            mirrorLevelTransform = transform;
        }
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

}
