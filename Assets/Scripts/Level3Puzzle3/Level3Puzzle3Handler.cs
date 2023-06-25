using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Level3Puzzle3Handler : MonoBehaviour
{
    GameObject puzzleTask;

    GameObject planeImageAnubis;
    GameObject planeImageHorus;
    GameObject planeImageMummy;
    GameObject planeImageNefertiti;
    GameObject planeImageTutankhamun;

    GameObject puzzleFrame1;
    GameObject puzzleFrame2;
    GameObject puzzleFrame3;

    Vector3 planeImageAnubisLocalPosition;
    Vector3 planeImageHorusLocalPosition;
    Vector3 planeImageMummyLocalPosition;
    Vector3 planeImageNefertitiLocalPosition;
    Vector3 planeImageTutankhamunLocalPosition;

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

        planeImageAnubis = transform.Find("PlaneImageAnubis").gameObject;
        planeImageHorus = transform.Find("PlaneImageHorus").gameObject;
        planeImageMummy = transform.Find("PlaneImageMummy").gameObject;
        planeImageNefertiti = transform.Find("PlaneImageNefertiti").gameObject;
        planeImageTutankhamun = transform.Find("PlaneImageTutankhamun").gameObject;

        puzzleFrame1 = transform.Find("Frame1").gameObject;
        puzzleFrame2 = transform.Find("Frame2").gameObject;
        puzzleFrame3 = transform.Find("Frame3").gameObject;

        planeImageAnubisLocalPosition = planeImageAnubis.transform.localPosition;
        planeImageHorusLocalPosition = planeImageHorus.transform.localPosition;
        planeImageMummyLocalPosition = planeImageMummy.transform.localPosition;
        planeImageNefertitiLocalPosition = planeImageNefertiti.transform.localPosition;
        planeImageTutankhamunLocalPosition = planeImageTutankhamun.transform.localPosition;

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
        if (other.gameObject.name == "Player" && isInRange && numberOfSelectedItems != 3)
        {
            // detect if player is looking at the interactable object with a Raycast
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity))
            {
                switch (hit.collider.gameObject.name)
                {
                    case "PlaneImageAnubis":
                        if (planeImageAnubisLocalPosition.Equals(planeImageAnubis.transform.localPosition))
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                Debug.Log("Here");
                                SelectImage(planeImageAnubis, "anubis");
                            }
                        }
                        break;
                    case "PlaneImageHorus":
                        if (planeImageHorusLocalPosition.Equals(planeImageHorus.transform.localPosition))
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                Debug.Log("Here");
                                SelectImage(planeImageHorus, "horus");
                            }
                        }
                        break;
                    case "PlaneImageMummy":
                        if (planeImageMummyLocalPosition.Equals(planeImageMummy.transform.localPosition))
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                Debug.Log("Here");
                                SelectImage(planeImageMummy, "mummy");
                            }
                        }
                        break;
                    case "PlaneImageNefertiti":
                        if (planeImageNefertitiLocalPosition.Equals(planeImageNefertiti.transform.localPosition))
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                Debug.Log("Here");
                                SelectImage(planeImageNefertiti, "nefertiti");
                            }
                        }
                        break;
                    case "PlaneImageTutankhamun":
                        if (planeImageTutankhamunLocalPosition.Equals(planeImageTutankhamun.transform.localPosition))
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                Debug.Log("Here");
                                SelectImage(planeImageTutankhamun, "tutankhamun");
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
        else
        {
            interactPromptText.text = "";
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
        if (selectedItems.ElementAt(0).Equals("nefertiti") && selectedItems.ElementAt(1).Equals("anubis") && selectedItems.ElementAt(2).Equals("tutankhamun"))
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
        planeImageAnubis.transform.localPosition = planeImageAnubisLocalPosition;
        planeImageHorus.transform.localPosition = planeImageHorusLocalPosition;
        planeImageMummy.transform.localPosition = planeImageMummyLocalPosition;
        planeImageNefertiti.transform.localPosition = planeImageNefertitiLocalPosition;
        planeImageTutankhamun.transform.localPosition = planeImageTutankhamunLocalPosition;

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
