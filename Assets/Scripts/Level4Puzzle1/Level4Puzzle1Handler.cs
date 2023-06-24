using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Level4Puzzle1Handler : MonoBehaviour
{
    GameObject puzzleTask;

    GameObject planeSymbolArm;
    GameObject planeSymbolEagle;
    GameObject planeSymbolFeather;
    GameObject planeSymbolJug;
    GameObject planeSymbolSnake;
    GameObject planeSymbolWick;

    GameObject puzzleFrame1;
    GameObject puzzleFrame2;
    GameObject puzzleFrame3;

    Vector3 planeSymbolArmLocalPosition;
    Vector3 planeSymbolEagleLocalPosition;
    Vector3 planeSymbolFeatherLocalPosition;
    Vector3 planeSymbolJugLocalPosition;
    Vector3 planeSymbolSnakeLocalPosition;
    Vector3 planeSymbolWickLocalPosition;

    Vector3 puzzleFrame1LocalPosition;
    Vector3 puzzleFrame2LocalPosition;
    Vector3 puzzleFrame3LocalPosition;

    // prompt related variables
    public GameObject interactPromptCanvas;
    public Text interactPromptText;
    string labelText = "[LMB] Interact";

    // player camera direction registration variables
    public GameObject mainCamera;
    public LayerMask interactableMask;
    public bool isInRange;

    GameObject puzzle;
    GameObject leftDoor;
    GameObject rightDoor;
    bool doorIsOpen;

    List<string> selectedItems;
    int numberOfSelectedItems;

    // Start is called before the first frame update
    void Start()
    {
        puzzleTask = this.gameObject;

        planeSymbolArm = transform.Find("PlaneSymbolArm").gameObject;
        planeSymbolEagle = transform.Find("PlaneSymbolEagle").gameObject;
        planeSymbolFeather = transform.Find("PlaneSymbolFeather").gameObject;
        planeSymbolJug = transform.Find("PlaneSymbolJug").gameObject;
        planeSymbolSnake = transform.Find("PlaneSymbolSnake").gameObject;
        planeSymbolWick = transform.Find("PlaneSymbolWick").gameObject;

        puzzleFrame1 = transform.Find("Frame1").gameObject;
        puzzleFrame2 = transform.Find("Frame2").gameObject;
        puzzleFrame3 = transform.Find("Frame3").gameObject;

        planeSymbolArmLocalPosition = planeSymbolArm.transform.localPosition;
        planeSymbolEagleLocalPosition = planeSymbolEagle.transform.localPosition;
        planeSymbolFeatherLocalPosition = planeSymbolFeather.transform.localPosition;
        planeSymbolJugLocalPosition = planeSymbolJug.transform.localPosition;
        planeSymbolSnakeLocalPosition = planeSymbolSnake.transform.localPosition;
        planeSymbolWickLocalPosition = planeSymbolWick.transform.localPosition;

        puzzleFrame1LocalPosition = puzzleFrame1.transform.localPosition;
        puzzleFrame2LocalPosition = puzzleFrame2.transform.localPosition;
        puzzleFrame3LocalPosition = puzzleFrame3.transform.localPosition;

        interactPromptCanvas = GameObject.Find("Canvas");
        interactPromptText = interactPromptCanvas.GetComponentInChildren<Text>();

        mainCamera = GameObject.Find("Main Camera");

        puzzle = puzzleTask.transform.parent.gameObject;
        leftDoor = puzzle.transform.GetChild(2).GetChild(0).gameObject;
        rightDoor = puzzle.transform.GetChild(2).GetChild(1).gameObject;
        doorIsOpen = false;

        selectedItems = new List<string>();
        numberOfSelectedItems = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
        interactPromptText.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && isInRange)
        {
            // detect if player is looking at the interactable object with a Raycast
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity))
            {
                switch (hit.collider.gameObject.name)
                {
                    case "PlaneSymbolArm":
                        if (planeSymbolArmLocalPosition.Equals(planeSymbolArm.transform.localPosition))
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                Debug.Log("Here");
                                SelectImage(planeSymbolArm, "arm");
                            }
                        }
                        break;
                    case "PlaneSymbolEagle":
                        if (planeSymbolEagleLocalPosition.Equals(planeSymbolEagle.transform.localPosition))
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                Debug.Log("Here");
                                SelectImage(planeSymbolEagle, "eagle");
                            }
                        }
                        break;
                    case "PlaneSymbolFeather":
                        if (planeSymbolFeatherLocalPosition.Equals(planeSymbolFeather.transform.localPosition))
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                Debug.Log("Here");
                                SelectImage(planeSymbolFeather, "feather");
                            }
                        }
                        break;
                    case "PlaneSymbolJug":
                        if (planeSymbolJugLocalPosition.Equals(planeSymbolJug.transform.localPosition))
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                Debug.Log("Here");
                                SelectImage(planeSymbolJug, "jug");
                            }
                        }
                        break;
                    case "PlaneSymbolSnake":
                        if (planeSymbolSnakeLocalPosition.Equals(planeSymbolSnake.transform.localPosition))
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                Debug.Log("Here");
                                SelectImage(planeSymbolSnake, "snake");
                            }
                        }
                        break;
                    case "PlaneSymbolWick":
                        if (planeSymbolWickLocalPosition.Equals(planeSymbolWick.transform.localPosition))
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                Debug.Log("Here");
                                SelectImage(planeSymbolWick, "wick");
                            }
                        }
                        break;
                    default:
                        interactPromptText.text = "";
                        break;
                }
            }
            else
            {
                interactPromptText.text = "";
            }
        }
    }

    private void SelectImage(GameObject gameObject, string name)
    {
        if (numberOfSelectedItems == 0) gameObject.transform.localPosition = puzzleFrame1LocalPosition;
        else if (numberOfSelectedItems == 1) gameObject.transform.localPosition = puzzleFrame2LocalPosition;
        else if (numberOfSelectedItems == 2) gameObject.transform.localPosition = puzzleFrame3LocalPosition;


        selectedItems.Add(name);
        numberOfSelectedItems += 1;

        if (numberOfSelectedItems == 3)
        {
            CheckSelectedItems();
        }
    }

    private void CheckSelectedItems()
    {
        if (selectedItems.ElementAt(0).Equals("snake") && selectedItems.ElementAt(1).Equals("eagle") && selectedItems.ElementAt(2).Equals("jug"))
        {
            Invoke("OpenDoor", 5);
        }
        else
        {
            Invoke("DeselectAllImages", 5);
        }
    }

    private void DeselectAllImages()
    {
        planeSymbolArm.transform.localPosition = planeSymbolArmLocalPosition;
        planeSymbolEagle.transform.localPosition = planeSymbolEagleLocalPosition;
        planeSymbolFeather.transform.localPosition = planeSymbolFeatherLocalPosition;
        planeSymbolJug.transform.localPosition = planeSymbolJugLocalPosition;
        planeSymbolSnake.transform.localPosition = planeSymbolSnakeLocalPosition;
        planeSymbolWick.transform.localPosition = planeSymbolWickLocalPosition;

        selectedItems.Clear();
        numberOfSelectedItems = 0;
    }

    private void OpenDoor()
    {
        if (!doorIsOpen)
        {
            leftDoor.transform.localPosition += Vector3.left * 12.24f;
            rightDoor.transform.localPosition += Vector3.right * 12.24f;
            doorIsOpen = true;
        }
    }
}
