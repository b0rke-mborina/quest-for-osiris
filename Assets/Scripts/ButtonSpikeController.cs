using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpikeController : MonoBehaviour
{
    [SerializeField] private Animator spikeAnim=null;
    private bool spikeDown= false;
    [SerializeField] private string downAnimationName = "SpikeDown";
    [SerializeField] private string upAnimationName = "SpikeUp";

    [SerializeField] private int waitTimer = 1;
    [SerializeField] private bool pauseInteraction= false;

    private IEnumerator PauseSpikeInteraction()
    {
        pauseInteraction = true;
        yield return new WaitForSeconds(waitTimer);
        pauseInteraction = false;
    }



    public void PlayAnimation()
    {
        if (!spikeDown && !pauseInteraction) 
        {
            spikeAnim.Play(downAnimationName, 0, 0.0f);
            spikeDown = true;
            StartCoroutine(PauseSpikeInteraction());
        }

        else if (spikeDown && !pauseInteraction)
        {
            spikeAnim.Play(upAnimationName, 0, 0.0f);
            spikeDown = false;
            StartCoroutine(PauseSpikeInteraction());
        }
    }
}
