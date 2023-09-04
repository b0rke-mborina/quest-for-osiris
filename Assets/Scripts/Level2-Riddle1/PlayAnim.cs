using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    //[SerializeField] private Animator myChest = null;
    //[SerializeField] private string chestOpen = "ChestOpen";
    [SerializeField] private KeySystem.KeyChestController chestController = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //myChest.Play(chestOpen, 0, 0.0f);
            if (other.CompareTag("Player"))
            {
                chestController.PlayAnimation();
            }
        }
    }

}
