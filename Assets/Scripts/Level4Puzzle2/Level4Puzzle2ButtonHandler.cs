using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4Puzzle2ButtonHandler : MonoBehaviour
{
    Level4Puzzle2MechanismHandler mechanismHandler;

    GameObject button;
    GameObject puzzleTask;
    GameObject puzzle;

    Vector3 buttonOriginalPosition;
    Vector3 buttonMovedPosition;

    // prompt related variables
    public GameObject interactPromptCanvas;
    public Text interactPromptText;
    string labelText = "[LMB] Press";

    // player camera direction registration variables
    public GameObject mainCamera;
    public LayerMask interactableMask;

    bool isInRangeOfButton;

    GameObject leftDoor;
    GameObject rightDoor;
    bool doorIsOpen;

    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject;
        puzzleTask = button.transform.parent.gameObject;
        puzzle = puzzleTask.transform.parent.gameObject;

        mechanismHandler = puzzleTask.transform.GetChild(0).GetComponent<Level4Puzzle2MechanismHandler>();

        buttonOriginalPosition = button.transform.localPosition;
        buttonMovedPosition = new Vector3(22.77f, 1.28f, -0.97f);


        interactPromptCanvas = GameObject.Find("Canvas");
        interactPromptText = interactPromptCanvas.GetComponentInChildren<Text>();

        mainCamera = GameObject.Find("Main Camera");

        isInRangeOfButton = false;

        leftDoor = puzzle.transform.GetChild(2).GetChild(0).gameObject;
        rightDoor = puzzle.transform.GetChild(2).GetChild(1).gameObject;
        doorIsOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isInRangeOfButton = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRangeOfButton = false;
        interactPromptText.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && mechanismHandler.isMechanismCompleted && isInRangeOfButton && !doorIsOpen)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Interactable")
                {
                    interactPromptText.text = labelText;

                    // interaction with LMB
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        HandleButtonPress();
                    }
                }
                else
                {
                    interactPromptText.text = "";
                }
            }
            else
            {
                interactPromptText.text = "";
            }
        }
        else
        {
            interactPromptText.text = "";
        }
    }

    // handle interaction
    private void HandleButtonPress()
    {
        if (!doorIsOpen)
        {

            button.transform.localPosition = buttonMovedPosition;
            doorIsOpen = true;
            Invoke("OpenDoor", 2);
        }
    }

    private void OpenDoor()
    {
        leftDoor.transform.localPosition += Vector3.left * 12.24f;
        rightDoor.transform.localPosition += Vector3.right * 12.24f;
    }
}
