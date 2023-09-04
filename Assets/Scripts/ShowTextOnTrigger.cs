using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowTextOnTrigger : MonoBehaviour
{
    public GameObject textGameObject;
    private bool isPlayerOnLocation;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !isPlayerOnLocation)
        {
            isPlayerOnLocation = true;
            textGameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player") && isPlayerOnLocation)
        {
            isPlayerOnLocation = false;
            textGameObject.SetActive(false);
        }
    }
}

