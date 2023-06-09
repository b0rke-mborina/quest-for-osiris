using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    // prompt related variables
    public GameObject interactPromptCanvas;
    public Text interactPromptText;
    string labelText = "[LMB] Open";

    // player camera direction registration variables
    public GameObject mainCamera;
    public LayerMask interactableMask;
    public bool isInRange;

    // object variables
    GameObject chest;
    GameObject chestTop;
    bool chestIsOpen;

    // Start is called before the first frame update
    void Start()
    {
        interactPromptCanvas = GameObject.Find("Canvas");
        interactPromptText = interactPromptCanvas.GetComponentInChildren<Text>();

        mainCamera = GameObject.Find("Main Camera");
        isInRange = false;

        chest = this.gameObject;
        chestTop = chest.transform.GetChild(0).gameObject;
        chestIsOpen = false;
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
        if (other.gameObject.name == "Player" && isInRange && !chestIsOpen)
        {
            // detect if player is looking at the interactable object with a Raycast
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Chest")
                {
                    interactPromptText.text = labelText;

                    // interaction with LMB
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Open();
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
    private void Open()
    {
        if (!chestIsOpen)
        {
            chestTop.transform.localRotation = Quaternion.Euler(-110f, 0f, 0f);
            chestIsOpen = true;
        }
    }
}
