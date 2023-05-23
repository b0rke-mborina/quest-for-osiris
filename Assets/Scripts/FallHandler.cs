using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerCameraController playerCameraController = other.gameObject.GetComponentInChildren<PlayerCameraController>();

        if (playerCameraController != null)
        {
            // Debug.Log("Fell!");
            Time.timeScale = 0;
            playerCameraController.isPaused = true;
        }
    }
}
