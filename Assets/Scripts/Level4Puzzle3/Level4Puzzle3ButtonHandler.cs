using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4Puzzle3ButtonHandler : MonoBehaviour
{
    Level4Puzzle3TubeSystemHandler tubeSystemHandler;

    GameObject button;
    GameObject puzzleTask;

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

    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject;
        puzzleTask = button.transform.parent.gameObject;
        tubeSystemHandler = puzzleTask.GetComponentInChildren<Level4Puzzle3TubeSystemHandler>();

        buttonOriginalPosition = button.transform.localPosition;
        buttonMovedPosition = new Vector3(-19.24f, 0f, 2.36f);

        interactPromptCanvas = GameObject.Find("Canvas");
        interactPromptText = interactPromptCanvas.GetComponentInChildren<Text>();

        mainCamera = GameObject.Find("Main Camera");

        isInRangeOfButton = false;
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
        if (other.gameObject.name == "Player" && isInRangeOfButton && !tubeSystemHandler.isInOriginalState && !tubeSystemHandler.isTubeSystemCompleted)
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
                        button.transform.localPosition = buttonMovedPosition;
                        tubeSystemHandler.SetToOriginalState();
                        Invoke("MoveButtonToOriginalPosition", 2);
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

    private void MoveButtonToOriginalPosition()
    {
        button.transform.localPosition = buttonOriginalPosition;
    }
}
