using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private float resetHeight;
    [SerializeField] private Transform respawnPoint;

    private void Update() {
        if (player.transform.position.y < resetHeight) {
            player.transform.position = respawnPoint.position;
            // player.GetComponent<PlayerMovement>().Reset();
        }
            
    }

}
