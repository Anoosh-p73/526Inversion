using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public enum GameState { InitialLevel, MirrorLevel, PauseGame}

    public GameState State { get; set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }
    private void OnDestroy() {
        // Reset the instance if the object is destroyed
        if (Instance == this) {
            Instance = null;
        }
    }
}
