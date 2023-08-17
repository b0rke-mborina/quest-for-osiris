using UnityEngine;
using System.Collections;

public class KeyInteraction : MonoBehaviour
{
    public GameObject chest02;
    public string openAnimationName = "ChestOpen"; // Set to the appropriate animation name
    private Animator chestAnimator;
    private Animator boatAnimator; // Reference to the boat's Animator
    private bool hasOpenedChest = false; // Flag to track if chest has been opened

    private void Start()
    {
        chestAnimator = chest02.GetComponent<Animator>();
        boatAnimator = GameObject.Find("boat").GetComponent<Animator>(); // Replace "YourBoatObjectName" with the actual name of your boat object
    }

    private IEnumerator PlayNextAnimation()
    {
        yield return new WaitForSeconds(chestAnimator.GetCurrentAnimatorStateInfo(0).length);
        boatAnimator.Play("goBoat"); // Make sure "goBoat" is the correct animation name
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasOpenedChest && other.CompareTag("Chest02Trigger"))
        {
            chestAnimator.Play(openAnimationName);
            hasOpenedChest = true;
            StartCoroutine(PlayNextAnimation());
        }
    }
}
