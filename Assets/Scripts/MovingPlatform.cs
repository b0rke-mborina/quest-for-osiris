using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private List<GameObject> playersOnPlatform = new List<GameObject>();
    private Vector3 lastPlatformPosition;

    private void Start()
    {
        lastPlatformPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playersOnPlatform.Contains(other.gameObject))
        {
            playersOnPlatform.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && playersOnPlatform.Contains(other.gameObject))
        {
            playersOnPlatform.Remove(other.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector3 platformMovement = transform.position - lastPlatformPosition;

        foreach (GameObject player in playersOnPlatform)
        {
            Vector3 playerRelativePosition = player.transform.position - transform.position;
            Vector3 playerLastRelativePosition = playerRelativePosition - platformMovement;

            player.transform.position = transform.position + playerRelativePosition;
            player.transform.position += platformMovement;
        }

        lastPlatformPosition = transform.position;
    }
}
