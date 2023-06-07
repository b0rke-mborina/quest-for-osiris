using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPromptHandler : MonoBehaviour
{
    // prompt related variables
    public GameObject interactPromptCanvas;
    public Text interactPromptText;
    string labelText = "[LMB] Interact";

    // player camera direction registration variables
    public GameObject mainCamera;
    public LayerMask interactableMask;
    public bool isInRange;

    // interaction variables
    GameObject interactable;
    GameObject puzzle;

    GameObject leftDoor;
    GameObject rightDoor;
    bool doorIsOpen;


    private void Start()
    {
        interactPromptCanvas = GameObject.Find("Canvas");
        interactPromptText = interactPromptCanvas.GetComponentInChildren<Text>();

        mainCamera = GameObject.Find("Main Camera");
        isInRange = false;

        interactable = this.gameObject;
        puzzle = interactable.transform.parent.gameObject;

        leftDoor = puzzle.transform.GetChild(2).GetChild(0).gameObject;
        rightDoor = puzzle.transform.GetChild(2).GetChild(1).gameObject;
        doorIsOpen = false;
    }

    // HANDLE RANGE (OnTriggerEnter, OnTriggerExit)

    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
        interactPromptText.text = "";
    }

    // handles prompt while in range
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && isInRange)
        {
            // detect if player is looking at the interactable object with a Raycast
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Interactable")
                {
                    interactPromptText.text = labelText;

                    // interaction with LMB
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Interact();
                    }
                }
                else
                {
                    interactPromptText.text = "";
                }
            }
        }
    }

    // handle interaction
    private void Interact()
    {
        if (!doorIsOpen)
        {
            leftDoor.transform.localPosition += Vector3.left * 12.24f;
            rightDoor.transform.localPosition += Vector3.right * 12.24f;
            doorIsOpen = true;
        }
    }
}
