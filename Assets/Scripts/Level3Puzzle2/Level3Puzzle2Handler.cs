using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Level3Puzzle2Handler : MonoBehaviour
{
    GameObject puzzleTask;
    GameObject puzzle;
    GameObject leftDoor;
    GameObject rightDoor;

    GameObject laserTurret1;
    GameObject laserTurret2;
    GameObject laserTurret3;
    GameObject laserTurret4;

    GameObject laserTurret1Canon;
    GameObject laserTurret2Canon;
    GameObject laserTurret3Canon;
    GameObject laserTurret4Canon;

    GameObject laserMark1;
    GameObject laserMark2;
    GameObject laserMark3;
    GameObject laserMark4;

    GameObject pyramid;

    Quaternion laserTurret1OriginalRotation;
    Quaternion laserTurret1RotatedRotation;

    LineRenderer laserTurret1CannonLineRenderer;
    LineRenderer laserTurret2CannonLineRenderer;
    LineRenderer laserTurret3CannonLineRenderer;
    LineRenderer laserTurret4CannonLineRenderer;

    Vector3 laserMark1ActivatedPosition;
    Vector3 laserMark1DeactivatedPosition;
    Vector3 laserMark2ActivatedPosition;
    Vector3 laserMark2DeactivatedPosition;
    Vector3 laserMark3ActivatedPosition;
    Vector3 laserMark3DeactivatedPosition;
    Vector3 laserMark4ActivatedPosition;
    Vector3 laserMark4DeactivatedPosition;

    Vector3 pyramidOriginalPosition;
    Vector3 pyramidMovedPosition;

    // prompt related variables
    public GameObject interactPromptCanvas;
    public Text interactPromptText;
    public GameObject mainCamera;

    string labelTextPress = "[LMB] Press";
    string labelTextRotate = "[LMB] Rotate";
    string labelTextUseMagic = "[F] Use magic";

    bool isInRangeOfPuzzleElements;
    bool isPyramidMoved;
    bool isLaserMark1Activated;
    bool isLaserMark2Activated;
    bool isLaserMark3Activated;
    bool isLaserMark4Activated;
    bool isMarkAcitvationEnabled;
    bool isLaserTurret1Rotated;
    bool isRotationEnabled;
    bool isLaserSystemCompleted;
    bool doorIsOpen;

    // Start is called before the first frame update
    void Start()
    {
        puzzleTask = this.gameObject;
        puzzle = puzzleTask.transform.parent.gameObject;
        leftDoor = puzzle.transform.GetChild(2).GetChild(0).gameObject;
        rightDoor = puzzle.transform.GetChild(2).GetChild(1).gameObject;

        laserTurret1 = transform.Find("LaserTurret1").gameObject;
        laserTurret2 = transform.Find("LaserTurret2").gameObject;
        laserTurret3 = transform.Find("LaserTurret3").gameObject;
        laserTurret4 = transform.Find("LaserTurret4").gameObject;

        laserTurret1Canon = transform.Find("LaserTurret1/Canon1").gameObject;
        laserTurret2Canon = transform.Find("LaserTurret2/Canon").gameObject;
        laserTurret3Canon = transform.Find("LaserTurret3/Canon").gameObject;
        laserTurret4Canon = transform.Find("LaserTurret4/Canon").gameObject;

        laserMark1 = transform.Find("LaserMark1").GetChild(0).gameObject;
        laserMark2 = transform.Find("LaserMark2").GetChild(0).gameObject;
        laserMark3 = transform.Find("LaserMark3").GetChild(0).gameObject;
        laserMark4 = transform.Find("LaserMark4").GetChild(0).gameObject;

        pyramid = transform.Find("Pyramid").gameObject;

        laserTurret1OriginalRotation = laserTurret1.transform.localRotation;
        laserTurret1RotatedRotation = Quaternion.Euler(0f, 0f, -90f);

        laserTurret1CannonLineRenderer = laserTurret1Canon.GetComponent<LineRenderer>();
        laserTurret1CannonLineRenderer.SetPosition(0, laserTurret1Canon.transform.position);
        laserTurret1CannonLineRenderer.SetPosition(1, laserTurret1Canon.transform.position + Vector3.right * 22.0f);

        laserTurret2CannonLineRenderer = laserTurret2Canon.GetComponent<LineRenderer>();
        laserTurret2CannonLineRenderer.SetPosition(0, laserTurret2Canon.transform.position);
        laserTurret2CannonLineRenderer.SetPosition(1, laserTurret2Canon.transform.position + Vector3.left * 10.0f);

        laserTurret3CannonLineRenderer = laserTurret3Canon.GetComponent<LineRenderer>();
        laserTurret3CannonLineRenderer.SetPosition(0, laserTurret3Canon.transform.position);
        laserTurret3CannonLineRenderer.SetPosition(1, laserTurret3Canon.transform.position + Vector3.left * 22.0f);

        laserTurret4CannonLineRenderer = laserTurret4Canon.GetComponent<LineRenderer>();
        laserTurret4CannonLineRenderer.SetPosition(0, laserTurret4Canon.transform.position);
        laserTurret4CannonLineRenderer.SetPosition(1, laserTurret4Canon.transform.position + Vector3.left * 15.0f);

        laserMark1ActivatedPosition = laserMark1.transform.localPosition;
        laserMark1DeactivatedPosition = new Vector3(0f, 1.780536f, -0.43f);
        laserMark2ActivatedPosition = laserMark2.transform.localPosition;
        laserMark2DeactivatedPosition = new Vector3(0f, 1.780536f, -0.43f);
        laserMark3ActivatedPosition = laserMark3.transform.localPosition;
        laserMark3DeactivatedPosition = new Vector3(0f, 1.780536f, -0.43f);
        laserMark4ActivatedPosition = new Vector3(0f, 1.780536f, -0.93f);
        laserMark4DeactivatedPosition = laserMark4.transform.localPosition;

        pyramidOriginalPosition = pyramid.transform.localPosition;
        pyramidMovedPosition = new Vector3(-10.928f, -12.65f, 1.3197f);

        interactPromptCanvas = GameObject.Find("Canvas");
        interactPromptText = interactPromptCanvas.GetComponentInChildren<Text>();
        mainCamera = GameObject.Find("Main Camera");

        isInRangeOfPuzzleElements = false;
        isPyramidMoved = false;
        isLaserMark1Activated = true;
        isLaserMark2Activated = true;
        isLaserMark3Activated = true;
        isLaserMark4Activated = false;
        isMarkAcitvationEnabled = true;
        isLaserTurret1Rotated = false;
        isRotationEnabled = true;
        isLaserSystemCompleted = false;
        doorIsOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isInRangeOfPuzzleElements = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRangeOfPuzzleElements = false;
        interactPromptText.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && isInRangeOfPuzzleElements && !doorIsOpen && !isLaserSystemCompleted)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity))
            {
                switch (hit.collider.gameObject.name)
                {
                    case "LaserMark1":
                        interactPromptText.text = labelTextPress;
                        if (Input.GetKey(KeyCode.Mouse0) && isMarkAcitvationEnabled)
                        {
                            ChangeMarkActivation(laserMark1, 1);
                        }
                        break;
                    case "LaserMark2":
                        interactPromptText.text = labelTextPress;
                        if (Input.GetKey(KeyCode.Mouse0) && isMarkAcitvationEnabled)
                        {
                            ChangeMarkActivation(laserMark2, 2);
                        }
                        break;
                    case "LaserMark3":
                        interactPromptText.text = labelTextPress;
                        if (Input.GetKey(KeyCode.Mouse0) && isMarkAcitvationEnabled)
                        {
                            ChangeMarkActivation(laserMark3, 3);
                        }
                        break;
                    case "LaserMark4":
                        interactPromptText.text = labelTextPress;
                        if (Input.GetKey(KeyCode.Mouse0) && isMarkAcitvationEnabled)
                        {
                            ChangeMarkActivation(laserMark4, 4);
                        }
                        break;
                    case "PyramidBase":
                        goto case "PyramidTip";
                    case "PyramidTip":
                        interactPromptText.text = labelTextUseMagic;
                        if (Input.GetKey(KeyCode.F))
                        {
                            MovePyramid();
                        }
                        break;
                    case "BodyBottom1":
                        goto case "Cannon1";
                    case "BodyMiddle1":
                        goto case "Cannon1";
                    case "Head1":
                        goto case "Cannon1";
                    case "Cannon1":
                        interactPromptText.text = labelTextRotate;
                        if (Input.GetKey(KeyCode.Mouse0) && isRotationEnabled)
                        {
                            RotateLaserTurret1();
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

    // Update is called once per frame
    void Update()
    {
        if (isLaserMark1Activated && isLaserMark2Activated && isLaserMark3Activated && isLaserMark4Activated && isLaserTurret1Rotated && isPyramidMoved && !doorIsOpen && !isLaserSystemCompleted)
        {
            isLaserSystemCompleted = true;
            OpenDoor();
        }
    }

    private void EnableMarkActivation()
    {
        isMarkAcitvationEnabled = true;
    }

    private void ChangeMarkActivation(GameObject mark, int indexOfMark)
    {
        switch (indexOfMark)
        {
            case 1:
                if (mark.transform.localPosition.Equals(laserMark1ActivatedPosition))
                {
                    mark.transform.localPosition = laserMark1DeactivatedPosition;
                    isLaserMark1Activated = false;
                }
                else
                {
                    mark.transform.localPosition = laserMark1ActivatedPosition;
                    isLaserMark1Activated = true;
                }
                break;
            case 2:
                if (mark.transform.localPosition.Equals(laserMark2ActivatedPosition))
                {
                    mark.transform.localPosition = laserMark2DeactivatedPosition;
                    isLaserMark2Activated = false;
                }
                else
                {
                    mark.transform.localPosition = laserMark2ActivatedPosition;
                    isLaserMark2Activated = true;
                }
                break;
            case 3:
                if (mark.transform.localPosition.Equals(laserMark3ActivatedPosition))
                {
                    mark.transform.localPosition = laserMark3DeactivatedPosition;
                    isLaserMark3Activated = false;
                }
                else
                {
                    mark.transform.localPosition = laserMark3ActivatedPosition;
                    isLaserMark3Activated = true;
                }
                break;
            case 4:
                if (mark.transform.localPosition.Equals(laserMark4ActivatedPosition))
                {
                    mark.transform.localPosition = laserMark4DeactivatedPosition;
                    isLaserMark4Activated = false;
                }
                else
                {
                    mark.transform.localPosition = laserMark4ActivatedPosition;
                    isLaserMark4Activated = true;
                }
                break;
        }

        isMarkAcitvationEnabled = false;
        Invoke("EnableMarkActivation", 0.5f);
    }

    private void MovePyramid()
    {
        pyramid.transform.localPosition = pyramidMovedPosition;
        laserTurret2CannonLineRenderer.SetPosition(1, laserTurret2Canon.transform.position + Vector3.left * 15.0f);
        isPyramidMoved = true;
    }

    private void EnableTurretRotation()
    {
        isRotationEnabled = true;
    }

    private void RotateLaserTurret1()
    {
        if (laserTurret1.transform.localRotation.Equals(laserTurret1OriginalRotation))
        {
            laserTurret1.transform.localRotation = laserTurret1RotatedRotation;
            laserTurret1CannonLineRenderer.SetPosition(0, laserTurret1Canon.transform.position);
            laserTurret1CannonLineRenderer.SetPosition(1, laserTurret1Canon.transform.position + Vector3.left * 22.0f);
            isLaserTurret1Rotated = true;
        }
        else
        {
            laserTurret1.transform.localRotation = laserTurret1OriginalRotation;
            laserTurret1CannonLineRenderer.SetPosition(0, laserTurret1Canon.transform.position);
            laserTurret1CannonLineRenderer.SetPosition(1, laserTurret1Canon.transform.position + Vector3.right * 22.0f);
            isLaserTurret1Rotated = false;
        }
        isRotationEnabled = false;
        Invoke("EnableTurretRotation", 0.5f);
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
