using UnityEngine;

public class ActivateCannon : MonoBehaviour
{
    private bool playerOnCannon = false;

    public bool IsPlayerOnCannon()
    {
        return playerOnCannon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnCannon = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnCannon = false;
        }
    }
}
