using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorWorldScript : MonoBehaviour
{

    [SerializeField] private GameObject levelParent;

    void InitializeMirrorWorld() {
        StartCoroutine(CreateMirror());
    }

    private void OnCollisionEnter(Collision collision) {
        print("Entering Mirror World");
        if(GameManager.Instance.State != GameManager.GameState.MirrorLevel)
            InitializeMirrorWorld();
    }

    
    IEnumerator CreateMirror() {
        float angle = 0;
        do {
            angle += 100 * Time.deltaTime;
            if (angle > 180) angle = 180;  // clamp
            levelParent.transform.rotation = Quaternion.Euler(0, angle, 0);
            yield return null;
        } while (angle < 180);
        GameManager.Instance.State = GameManager.GameState.MirrorLevel;
    }

}
