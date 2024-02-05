using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

    [SerializeField] private GameObject LevelParent;

    // Start is called before the first frame update
    void Start() {
        if (LevelParent != null) {
            AddScriptToChildren(LevelParent);
        }
    }

    void AddScriptToChildren(GameObject parent) {
        foreach (Transform child in parent.transform) {
            // Check the tag of each child
            if (child.CompareTag("Stationary")) {
                child.gameObject.AddComponent<StationaryPlatformScript>();
            } else if (child.CompareTag("Moving")) {
                child.gameObject.AddComponent<MovingPlatformScript>();
            }
        }
    }
}
