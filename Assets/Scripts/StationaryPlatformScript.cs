using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class StationaryPlatformScript : MonoBehaviour
{
    private bool touched = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!touched || GameManager.Instance.State == GameManager.GameState.InitialLevel) {
            
        } else if (GameManager.Instance.State == GameManager.GameState.MirrorLevel && touched) {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(GameManager.Instance.State != GameManager.GameState.MirrorLevel)
            touched = true;
    }

}
