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
    GameObject leftDoor;
    GameObject rightDoor;

    private void Start()
    {
        interactPromptCanvas = GameObject.Find("Canvas");
        interactPromptText = interactPromptCanvas.GetComponentInChildren<Text>();

        mainCamera = GameObject.Find("Main Camera");
        isInRange = false;

        leftDoor = GameObject.Find("ColumnSquare_MediumLeft");
        rightDoor = GameObject.Find("ColumnSquare_MediumRight");
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
        // Debug.Log("Interacted");
        leftDoor.transform.position = new Vector3(-20f, 0f, 31.9f);
        rightDoor.transform.position = new Vector3(20f, 0f, 31.9f);
    }
}
