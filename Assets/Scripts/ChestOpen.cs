using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] private Animator chestAnim = null;
    private bool chestOpen = false;

    [SerializeField] private string openChest = "Open";

    [SerializeField] private int waitTimer = 1;
    [SerializeField] private bool pauseInteraction = false;

    private IEnumerator PauseChestInteraction()
    {
        pauseInteraction = true;
        yield return new WaitForSeconds(waitTimer);
        pauseInteraction = false;
    }



    public void PlayAnimation()
    {
        if (!chestOpen && !pauseInteraction)
        {
            chestAnim.Play(openChest, 0, 0.0f);
            chestOpen = true;
            StartCoroutine(PauseChestInteraction());
        }
    }
}