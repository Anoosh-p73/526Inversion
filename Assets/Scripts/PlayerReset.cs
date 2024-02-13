using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private float resetHeight;
    
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Transform mirrorRespawnPoint;

    private void Update() {
        if (player.transform.position.y < resetHeight) {
            if(GameManager.Instance.State == GameManager.GameState.InitialLevel)
                player.transform.position = respawnPoint.position;
            else if(GameManager.Instance.State == GameManager.GameState.MirrorLevel)
                player.transform.position = mirrorRespawnPoint.position;
            player.GetComponent<PlayerMovement>().Reset();
        }
            
    }

}
