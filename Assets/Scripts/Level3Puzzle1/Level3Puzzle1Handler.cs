using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

public class Level3Puzzle1Handler : MonoBehaviour
{
    GameObject puzzleTask;

    GameObject planeSymbolSun;
    GameObject planeSymbolHorusEye;
    GameObject planeSymbolSarcophagus;

    GameObject planeGroup1Name1;
    GameObject planeGroup1Name2;
    GameObject planeGroup1Name3;

    GameObject planeGroup2Name1;
    GameObject planeGroup2Name2;
    GameObject planeGroup2Name3;
    
    GameObject planeGroup3Name1;
    GameObject planeGroup3Name2;
    GameObject planeGroup3Name3;

    Light lightGroup1Name1;
    Light lightGroup1Name2;
    Light lightGroup1Name3;

    Light lightGroup2Name1;
    Light lightGroup2Name2;
    Light lightGroup2Name3;

    Light lightGroup3Name1;
    Light lightGroup3Name2;
    Light lightGroup3Name3;

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

    bool canSelect;
    int selectedItemGroup1;
    int selectedItemGroup2;
    int selectedItemGroup3;
    int numberOfSelectedItems;

    // Start is called before the first frame update
    void Start()
    {
        puzzleTask = this.gameObject;

        planeSymbolSun = transform.Find("Symbols/PlaneSymbol1Sun").gameObject;
        planeSymbolHorusEye = transform.Find("Symbols/PlaneSymbol2HorusEye").gameObject;
        planeSymbolSarcophagus = transform.Find("Symbols/PlaneSymbol3Sarcophagus").gameObject;

        planeGroup1Name1 = transform.Find("Group1Names/PlaneGroup1Name1").gameObject;
        planeGroup1Name2 = transform.Find("Group1Names/PlaneGroup1Name2").gameObject;
        planeGroup1Name3 = transform.Find("Group1Names/PlaneGroup1Name3").gameObject;

        planeGroup2Name1 = transform.Find("Group2Names/PlaneGroup2Name1").gameObject;
        planeGroup2Name2 = transform.Find("Group2Names/PlaneGroup2Name2").gameObject;
        planeGroup2Name3 = transform.Find("Group2Names/PlaneGroup2Name3").gameObject;

        planeGroup3Name1 = transform.Find("Group3Names/PlaneGroup3Name1").gameObject;
        planeGroup3Name2 = transform.Find("Group3Names/PlaneGroup3Name2").gameObject;
        planeGroup3Name3 = transform.Find("Group3Names/PlaneGroup3Name3").gameObject;

        lightGroup1Name1 = (Light)transform.Find("Group1Names/LightGroup1Name1").gameObject.GetComponent("Light");
        lightGroup1Name2 = (Light)transform.Find("Group1Names/LightGroup1Name2").gameObject.GetComponent("Light");
        lightGroup1Name3 = (Light)transform.Find("Group1Names/LightGroup1Name3").gameObject.GetComponent("Light");

        lightGroup2Name1 = (Light)transform.Find("Group2Names/LightGroup2Name1").gameObject.GetComponent("Light");
        lightGroup2Name2 = (Light)transform.Find("Group2Names/LightGroup2Name2").gameObject.GetComponent("Light");
        lightGroup2Name3 = (Light)transform.Find("Group2Names/LightGroup2Name3").gameObject.GetComponent("Light");

        lightGroup3Name1 = (Light)transform.Find("Group3Names/LightGroup3Name1").gameObject.GetComponent("Light");
        lightGroup3Name2 = (Light)transform.Find("Group3Names/LightGroup3Name2").gameObject.GetComponent("Light");
        lightGroup3Name3 = (Light)transform.Find("Group3Names/LightGroup3Name3").gameObject.GetComponent("Light");

        interactPromptCanvas = GameObject.Find("Canvas");
        interactPromptText = interactPromptCanvas.GetComponentInChildren<Text>();

        mainCamera = GameObject.Find("Main Camera");

        puzzle = puzzleTask.transform.parent.gameObject;
        leftDoor = puzzle.transform.GetChild(2).GetChild(0).gameObject;
        rightDoor = puzzle.transform.GetChild(2).GetChild(1).gameObject;
        doorIsOpen = false;

        canSelect = true;
        selectedItemGroup1 = 0;
        selectedItemGroup2 = 0;
        selectedItemGroup3 = 0;
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
        if (other.gameObject.name == "Player" && isInRange && canSelect && numberOfSelectedItems != 3)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity))
            {
                switch (hit.collider.gameObject.name)
                {
                    case "PlaneGroup1Name1":
                        if (selectedItemGroup1 != 1)
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                if (selectedItemGroup1 != 0) TurnOffLightsForGroup(1);
                                lightGroup1Name1.enabled = true;
                                if (selectedItemGroup1 == 0) numberOfSelectedItems += 1;
                                selectedItemGroup1 = 1;
                                if (numberOfSelectedItems == 3 && canSelect)
                                {
                                    canSelect = false;
                                    StartHandlingSelection();
                                }
                            }
                        }
                        break;
                    case "PlaneGroup1Name2":
                        if (selectedItemGroup1 != 2)
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                if (selectedItemGroup1 != 0) TurnOffLightsForGroup(1);
                                lightGroup1Name2.enabled = true;
                                if (selectedItemGroup1 == 0) numberOfSelectedItems += 1;
                                selectedItemGroup1 = 2;
                                if (numberOfSelectedItems == 3 && canSelect)
                                {
                                    canSelect = false;
                                    StartHandlingSelection();
                                }
                            }
                        }
                        break;
                    case "PlaneGroup1Name3":
                        if (selectedItemGroup1 != 3)
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                if (selectedItemGroup1 != 0) TurnOffLightsForGroup(1);
                                lightGroup1Name3.enabled = true;
                                if (selectedItemGroup1 == 0) numberOfSelectedItems += 1;
                                selectedItemGroup1 = 3;
                                if (numberOfSelectedItems == 3 && canSelect)
                                {
                                    canSelect = false;
                                    StartHandlingSelection();
                                }
                            }
                        }
                        break;
                    case "PlaneGroup2Name1":
                        if (selectedItemGroup2 != 1)
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                if (selectedItemGroup2 != 0) TurnOffLightsForGroup(2);
                                lightGroup2Name1.enabled = true;
                                if (selectedItemGroup2 == 0) numberOfSelectedItems += 1;
                                selectedItemGroup2 = 1;
                                if (numberOfSelectedItems == 3 && canSelect)
                                {
                                    canSelect = false;
                                    StartHandlingSelection();
                                }
                            }
                        }
                        break;
                    case "PlaneGroup2Name2":
                        if (selectedItemGroup2 != 2)
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                if (selectedItemGroup2 != 0) TurnOffLightsForGroup(2);
                                lightGroup2Name2.enabled = true;
                                if (selectedItemGroup2 == 0) numberOfSelectedItems += 1;
                                selectedItemGroup2 = 2;
                                if (numberOfSelectedItems == 3 && canSelect)
                                {
                                    canSelect = false;
                                    StartHandlingSelection();
                                }
                            }
                        }
                        break;
                    case "PlaneGroup2Name3":
                        if (selectedItemGroup2 != 3)
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                if (selectedItemGroup2 != 0) TurnOffLightsForGroup(2);
                                lightGroup2Name3.enabled = true;
                                if (selectedItemGroup2 == 0) numberOfSelectedItems += 1;
                                selectedItemGroup2 = 3;
                                if (numberOfSelectedItems == 3 && canSelect)
                                {
                                    canSelect = false;
                                    StartHandlingSelection();
                                }
                            }
                        }
                        break;
                    case "PlaneGroup3Name1":
                        if (selectedItemGroup3 != 1)
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                if (selectedItemGroup3 != 0) TurnOffLightsForGroup(3);
                                lightGroup3Name1.enabled = true;
                                if (selectedItemGroup3 == 0) numberOfSelectedItems += 1;
                                selectedItemGroup3 = 1;
                                if (numberOfSelectedItems == 3 && canSelect)
                                {
                                    canSelect = false;
                                    StartHandlingSelection();
                                }
                            }
                        }
                        break;
                    case "PlaneGroup3Name2":
                        if (selectedItemGroup3 != 2)
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                if (selectedItemGroup3 != 0) TurnOffLightsForGroup(3);
                                lightGroup3Name2.enabled = true;
                                if (selectedItemGroup3 == 0) numberOfSelectedItems += 1;
                                selectedItemGroup3 = 2;
                                if (numberOfSelectedItems == 3 && canSelect)
                                {
                                    canSelect = false;
                                    StartHandlingSelection();
                                }
                            }
                        }
                        break;
                    case "PlaneGroup3Name3":
                        if (selectedItemGroup3 != 3)
                        {
                            interactPromptText.text = labelText;

                            // interaction with LMB
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                if (selectedItemGroup3 != 0) TurnOffLightsForGroup(3);
                                lightGroup3Name3.enabled = true;
                                if (selectedItemGroup3 == 0) numberOfSelectedItems += 1;
                                selectedItemGroup3 = 3;
                                if (numberOfSelectedItems == 3 && canSelect)
                                {
                                    canSelect = false;
                                    StartHandlingSelection();
                                }
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

    private void TurnOffLightsForGroup(int group)
    {
        switch (group)
        {
            case 1:
                lightGroup1Name1.enabled = false;
                lightGroup1Name2.enabled = false;
                lightGroup1Name3.enabled = false;
                break;
            case 2:
                lightGroup2Name1.enabled = false;
                lightGroup2Name2.enabled = false;
                lightGroup2Name3.enabled = false;
                break;
            case 3:
                lightGroup3Name1.enabled = false;
                lightGroup3Name2.enabled = false;
                lightGroup3Name3.enabled = false;
                break;
        }
    }

    private void StartHandlingSelection()
    {
        interactPromptText.text = "";
        Invoke("FinishHandlingSelection", 5);
    }

    private void FinishHandlingSelection()
    {
        if (selectedItemGroup1 == 3 && selectedItemGroup2 == 1 && selectedItemGroup3 == 1)
        {
            OpenDoor();
        }
        else
        {
            canSelect = true;
        }
        DeselectAllNames();
    }

    private void DeselectAllNames()
    {
        lightGroup1Name1.enabled = false;
        lightGroup1Name2.enabled = false;
        lightGroup1Name3.enabled = false;

        lightGroup2Name1.enabled = false;
        lightGroup2Name2.enabled = false;
        lightGroup2Name3.enabled = false;

        lightGroup3Name1.enabled = false;
        lightGroup3Name2.enabled = false;
        lightGroup3Name3.enabled = false;

        numberOfSelectedItems = 0;
        selectedItemGroup1 = 0;
        selectedItemGroup2 = 0;
        selectedItemGroup3 = 0;
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
