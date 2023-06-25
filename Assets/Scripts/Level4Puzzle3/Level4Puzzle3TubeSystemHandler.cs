using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Level4Puzzle3TubeSystemHandler : MonoBehaviour
{
    GameObject tubeSystem;
    GameObject puzzleTask;
    GameObject puzzle;

    GameObject barrier1;
    GameObject barrierRight1;
    GameObject barrierRight2;
    GameObject barrierLeft1;
    GameObject barrierLeft2;
    // GameObject barrierLeft3;

    Renderer tankTopRenderer;
    Renderer tubeMiddlePart1Renderer;
    Renderer tubeMiddlePart2Renderer;
    Renderer tubeMiddlePart3Renderer;
    Renderer tubeRightPart1Renderer;
    Renderer tubeRightPart2Renderer;
    Renderer tubeRightPart3Renderer;
    Renderer tubeRightPart4Renderer;
    Renderer tubeRightPart5Renderer;
    Renderer tubeRightPart6Renderer;
    Renderer tubeLeftPart1Renderer;
    Renderer tubeLeftPart2Renderer;
    Renderer tubeLeftPart3Renderer;
    Renderer tubeLeftPart4Renderer;
    Renderer tubeLeftPart5Renderer;
    Renderer tubeLeftPart6Renderer;
    Renderer tubeLeftRightPart1Renderer;
    Renderer tubeLeftRightPart2Renderer;
    Renderer tubeLeftRightPart3Renderer;
    Renderer tubeLeftLeftPart1Renderer;
    Renderer tubeLeftLeftPart2Renderer;
    Renderer tubeLeftLeftPart3Renderer;
    Renderer tankBottomPart2Renderer;
    Renderer tankBottomPart1Renderer;

    Vector3 barrier1OriginalPosition;
    Vector3 barrier1MovedPosition;
    Vector3 barrierRight1OriginalPosition;
    Vector3 barrierRight1MovedPosition;
    Vector3 barrierRight2OriginalPosition;
    Vector3 barrierRight2MovedPosition;
    Vector3 barrierLeft1OriginalPosition;
    Vector3 barrierLeft1MovedPosition;
    Vector3 barrierLeft2OriginalPosition;
    Vector3 barrierLeft2MovedPosition;

    // prompt related variables
    public GameObject interactPromptCanvas;
    public Text interactPromptText;
    string labelText = "[LMB] Move";

    // player camera direction registration variables
    public GameObject mainCamera;
    public LayerMask interactableMask;

    public bool isInOriginalState;
    public bool isInRangeOfTubeSystemBarriers;
    public bool canMoveBarriers;
    public bool isTubeSystemCompleted;

    bool hasWaterPassedBarrier1;
    bool hasWaterPassedBarrierRight1;
    bool hasWaterRightReachedTankBottom;
    bool hasWaterPassedBarrierLeft1;
    bool hasWaterGoneLeft;
    bool hasWaterLeftReachedTankBottom;

    GameObject leftDoor;
    GameObject rightDoor;
    bool doorIsOpen;

    // Start is called before the first frame update
    void Start()
    {
        tubeSystem = this.gameObject;
        puzzleTask = tubeSystem.transform.parent.gameObject;
        puzzle = puzzleTask.transform.parent.gameObject;

        barrier1 = transform.Find("Barrier1").gameObject;
        barrierRight1 = transform.Find("BarrierRight1").gameObject;
        barrierRight2 = transform.Find("BarrierRight2").gameObject;
        barrierLeft1 = transform.Find("BarrierLeft1").gameObject;
        barrierLeft2 = transform.Find("BarrierLeft2").gameObject;

        tankTopRenderer = transform.Find("TankTop").GetComponent<Renderer>();
        tubeMiddlePart1Renderer = transform.Find("TubeMiddlePart1").GetComponent<Renderer>();
        tubeMiddlePart2Renderer = transform.Find("TubeMiddlePart2").GetComponent<Renderer>();
        tubeMiddlePart3Renderer = transform.Find("TubeMiddlePart3").GetComponent<Renderer>();
        tubeRightPart1Renderer = transform.Find("TubeRightPart1").GetComponent<Renderer>();
        tubeRightPart2Renderer = transform.Find("TubeRightPart2").GetComponent<Renderer>();
        tubeRightPart3Renderer = transform.Find("TubeRightPart3").GetComponent<Renderer>();
        tubeRightPart4Renderer = transform.Find("TubeRightPart4").GetComponent<Renderer>();
        tubeRightPart5Renderer = transform.Find("TubeRightPart5").GetComponent<Renderer>();
        tubeRightPart6Renderer = transform.Find("TubeRightPart6").GetComponent<Renderer>();
        tubeLeftPart1Renderer = transform.Find("TubeLeftPart1").GetComponent<Renderer>();
        tubeLeftPart2Renderer = transform.Find("TubeLeftPart2").GetComponent<Renderer>();
        tubeLeftPart3Renderer = transform.Find("TubeLeftPart3").GetComponent<Renderer>();
        tubeLeftPart4Renderer = transform.Find("TubeLeftPart4").GetComponent<Renderer>();
        tubeLeftPart5Renderer = transform.Find("TubeLeftPart5").GetComponent<Renderer>();
        tubeLeftPart6Renderer = transform.Find("TubeLeftPart6").GetComponent<Renderer>();
        tubeLeftRightPart1Renderer = transform.Find("TubeLeftRightPart1").GetComponent<Renderer>();
        tubeLeftRightPart2Renderer = transform.Find("TubeLeftRightPart2").GetComponent<Renderer>();
        tubeLeftRightPart3Renderer = transform.Find("TubeLeftRightPart3").GetComponent<Renderer>();
        tubeLeftLeftPart1Renderer = transform.Find("TubeLeftLeftPart1").GetComponent<Renderer>();
        tubeLeftLeftPart2Renderer = transform.Find("TubeLeftLeftPart2").GetComponent<Renderer>();
        tubeLeftLeftPart3Renderer = transform.Find("TubeLeftLeftPart3").GetComponent<Renderer>();
        tankBottomPart2Renderer = transform.Find("TankBottomPart2").GetComponent<Renderer>();
        tankBottomPart1Renderer = transform.Find("TankBottomPart1").GetComponent<Renderer>();

        barrier1OriginalPosition = barrier1.transform.localPosition;
        barrier1MovedPosition = new Vector3(-1.45f, 4.65f, -7.762f);
        barrierRight1OriginalPosition = barrierRight1.transform.localPosition;
        barrierRight1MovedPosition = new Vector3(1.36f, 4.98f, -7.762f);
        barrierRight2OriginalPosition = barrierRight2.transform.localPosition;
        barrierRight2MovedPosition = new Vector3(3.22f, 1.216f, -7.762f);
        barrierLeft1OriginalPosition = barrierLeft1.transform.localPosition;
        barrierLeft1MovedPosition = new Vector3(-1.15f, 1.3f, -7.762f);
        barrierLeft2OriginalPosition = barrierLeft2.transform.localPosition;
        barrierLeft2MovedPosition = new Vector3(-2.168f, 1.3f, -7.762f);

        interactPromptCanvas = GameObject.Find("Canvas");
        interactPromptText = interactPromptCanvas.GetComponentInChildren<Text>();

        mainCamera = GameObject.Find("Main Camera");

        SetToOriginalState();

        isInRangeOfTubeSystemBarriers = false;
        canMoveBarriers = true;
        isTubeSystemCompleted = false;

        hasWaterPassedBarrier1 = false;
        hasWaterPassedBarrierRight1 = false;
        hasWaterRightReachedTankBottom = false;
        hasWaterPassedBarrierLeft1 = false;
        hasWaterGoneLeft = false;
        hasWaterLeftReachedTankBottom = false;

        leftDoor = puzzle.transform.GetChild(2).GetChild(0).gameObject;
        rightDoor = puzzle.transform.GetChild(2).GetChild(1).gameObject;
        doorIsOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isInRangeOfTubeSystemBarriers = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRangeOfTubeSystemBarriers = false;
        interactPromptText.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && isInRangeOfTubeSystemBarriers && canMoveBarriers && !doorIsOpen && !isTubeSystemCompleted)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity))
            {
                switch (hit.collider.gameObject.name)
                {
                    case "Barrier1":
                        interactPromptText.text = labelText;
                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            MoveBarrier(barrier1);
                        }
                        break;
                    case "BarrierRight1":
                        interactPromptText.text = labelText;
                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            MoveBarrier(barrierRight1);
                        }
                        break;
                    case "BarrierRight2":
                        interactPromptText.text = labelText;
                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            MoveBarrier(barrierRight2);
                        }
                        break;
                    case "BarrierLeft1":
                        interactPromptText.text = labelText;
                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            MoveBarrier(barrierLeft1);
                        }
                        break;
                    case "BarrierLeft2":
                        interactPromptText.text = labelText;
                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            MoveBarrier(barrierLeft2);
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

    public void SetToOriginalState()
    {
        barrier1.transform.localPosition = barrier1OriginalPosition;
        barrierRight1.transform.localPosition = barrierRight1OriginalPosition;
        barrierRight2.transform.localPosition = barrierRight2OriginalPosition;
        barrierLeft1.transform.localPosition = barrierLeft1OriginalPosition;
        barrierLeft2.transform.localPosition = barrierLeft2OriginalPosition;

        SetLightBlueColor(tankTopRenderer);
        SetLightBlueColor(tubeMiddlePart1Renderer);
        SetGreyColor(tubeMiddlePart2Renderer);
        SetGreyColor(tubeMiddlePart3Renderer);
        SetGreyColor(tubeRightPart1Renderer);
        SetGreyColor(tubeRightPart2Renderer);
        SetGreyColor(tubeRightPart3Renderer);
        SetGreyColor(tubeRightPart4Renderer);
        SetGreyColor(tubeRightPart5Renderer);
        SetGreyColor(tubeRightPart6Renderer);
        SetGreyColor(tubeLeftPart1Renderer);
        SetGreyColor(tubeLeftPart2Renderer);
        SetGreyColor(tubeLeftPart3Renderer);
        SetGreyColor(tubeLeftPart4Renderer);
        SetGreyColor(tubeLeftPart5Renderer);
        SetGreyColor(tubeLeftPart6Renderer);
        SetGreyColor(tubeLeftRightPart1Renderer);
        SetGreyColor(tubeLeftRightPart2Renderer);
        SetGreyColor(tubeLeftRightPart3Renderer);
        SetGreyColor(tubeLeftLeftPart1Renderer);
        SetGreyColor(tubeLeftLeftPart2Renderer);
        SetGreyColor(tubeLeftLeftPart3Renderer);
        SetGreyColor(tankBottomPart2Renderer);
        SetGreyColor(tankBottomPart1Renderer);
        isInOriginalState = true;

        hasWaterPassedBarrier1 = false;
        hasWaterPassedBarrierRight1 = false;
        hasWaterRightReachedTankBottom = false;
        hasWaterPassedBarrierLeft1 = false;
        hasWaterGoneLeft = false;
        hasWaterLeftReachedTankBottom = false;
    }

    private void MoveWaterBelowBarrier1()
    {
        SetGreyColor(tankTopRenderer);
        SetGreyColor(tubeMiddlePart1Renderer);
        SetLightBlueColor(tubeMiddlePart2Renderer);
        SetLightBlueColor(tubeMiddlePart3Renderer);
        SetLightBlueColor(tubeRightPart1Renderer);
        SetLightBlueColor(tubeLeftPart1Renderer);

        isInOriginalState = false;
        hasWaterPassedBarrier1 = true;

        if (barrierRight1.transform.localPosition.Equals(barrierRight1MovedPosition))
        {
            Invoke("MoveWaterBelowBarrierRight1", 0.5f);
        }
        if (barrierLeft1.transform.localPosition.Equals(barrierLeft1MovedPosition)) Invoke("MoveWaterBelowBarrierLeft1", 0.5f);
    }

    private void MoveWaterBelowBarrierRight1()
    {
        SetGreyColor(tubeMiddlePart2Renderer);
        SetLightBlueColor(tubeRightPart2Renderer);
        SetLightBlueColor(tubeRightPart3Renderer);
        SetLightBlueColor(tubeRightPart4Renderer);
        SetLightBlueColor(tubeRightPart5Renderer);

        if (hasWaterPassedBarrierLeft1)
        {
            SetGreyColor(tubeMiddlePart3Renderer);
            SetGreyColor(tubeRightPart1Renderer);
            SetGreyColor(tubeLeftPart1Renderer);
        }

        isInOriginalState = false;
        hasWaterPassedBarrierRight1 = true;

        if (hasWaterPassedBarrier1 && barrierRight2.transform.localPosition.Equals(barrierRight2MovedPosition)) Invoke("MoveWaterBelowBarrierRight2", 0.5f);
    }

    private void MoveWaterBelowBarrierRight2()
    {
        SetGreyColor(tubeRightPart2Renderer);
        SetGreyColor(tubeRightPart3Renderer);
        SetLightBlueColor(tubeRightPart6Renderer);

        hasWaterRightReachedTankBottom = true;

        Invoke("MoveWaterRightTankBottom", 0.5f);
    }

    private void MoveWaterRightTankBottom()
    {
        SetGreyColor(tubeRightPart4Renderer);
        SetGreyColor(tubeRightPart5Renderer);
        SetGreyColor(tubeRightPart6Renderer);
        SetLightBlueColor(tankBottomPart1Renderer);
        if (hasWaterLeftReachedTankBottom) SetLightBlueColor(tankBottomPart2Renderer);

        hasWaterRightReachedTankBottom = true;
    }

    private void MoveWaterBelowBarrierLeft1()
    {
        SetLightBlueColor(tubeMiddlePart2Renderer);
        SetLightBlueColor(tubeLeftPart2Renderer);
        SetLightBlueColor(tubeLeftPart3Renderer);

        if (hasWaterPassedBarrierRight1)
        {
            SetGreyColor(tubeMiddlePart3Renderer);
            SetGreyColor(tubeRightPart1Renderer);
            SetGreyColor(tubeLeftPart1Renderer);
        }

        Invoke("UpdateWaterBelowBarrierLeft1", 0.5f);

        hasWaterPassedBarrierLeft1 = true;

        if (hasWaterPassedBarrier1) Invoke("UpdateWaterBelowBarrierLeft1", 0.5f);
    }
    
    private void UpdateWaterBelowBarrierLeft1()
    {
        SetGreyColor(tubeMiddlePart2Renderer);
        SetGreyColor(tubeLeftPart2Renderer);
        SetGreyColor(tubeLeftPart3Renderer);
        SetLightBlueColor(tubeLeftPart4Renderer);
        SetLightBlueColor(tubeLeftPart5Renderer);
        SetLightBlueColor(tubeLeftPart6Renderer);

        if (hasWaterPassedBarrier1 && hasWaterPassedBarrierLeft1 && barrierLeft2.transform.localPosition.Equals(barrierLeft2OriginalPosition)) Invoke("MakeWaterGoLeft", 0.5f);
    }

    private void MakeWaterGoLeft()
    {
        SetGreyColor(tubeLeftPart4Renderer);
        SetLightBlueColor(tubeLeftLeftPart1Renderer);
        SetLightBlueColor(tubeLeftLeftPart2Renderer);
        SetLightBlueColor(tubeLeftLeftPart3Renderer);

        hasWaterGoneLeft = true;
    }

    private void StartMovingWaterLeftTankBottom()
    {
        SetGreyColor(tubeLeftPart4Renderer);
        SetGreyColor(tubeLeftPart5Renderer);
        SetGreyColor(tubeLeftPart6Renderer);
        SetLightBlueColor(tubeLeftRightPart1Renderer);
        SetLightBlueColor(tubeLeftRightPart2Renderer);
        SetLightBlueColor(tubeLeftRightPart3Renderer);

        Invoke("FinishMovingWaterLeftTankBottom", 1f);
        hasWaterLeftReachedTankBottom = true;
    }

    private void FinishMovingWaterLeftTankBottom()
    {
        SetGreyColor(tubeLeftRightPart1Renderer);
        SetGreyColor(tubeLeftRightPart2Renderer);
        SetGreyColor(tubeLeftRightPart3Renderer);
        SetLightBlueColor(tankBottomPart1Renderer);
        if (hasWaterRightReachedTankBottom) SetLightBlueColor(tankBottomPart2Renderer);

        hasWaterLeftReachedTankBottom = true;
    }

    public void SetLightBlueColor(Renderer renderer)
    {
        Color newColor = new Color(67/255f, 179/255f, 190/255f, 1f);
        renderer.material.color = newColor;
    }

    public void SetGreyColor(Renderer renderer)
    {
        Color newColor = new Color(130/255f, 130/255f, 130/255f, 1f);
        renderer.material.color = newColor;
    }

    private void EnableBarrierMoving()
    {
        canMoveBarriers = true;
    }

    public void MoveBarrier(GameObject barrier)
    {
        Vector3 originalPosition = Vector3.zero;
        Vector3 movedPosition = Vector3.zero;
        bool extendBarrierMoveCooldown = false;

        switch (barrier.name)
        {
            case "Barrier1":
                originalPosition = barrier1OriginalPosition;
                movedPosition = barrier1MovedPosition;
                if (isInOriginalState && barrier.transform.localPosition.Equals(barrier1OriginalPosition)) Invoke("MoveWaterBelowBarrier1", 0.5f);
                break;
            case "BarrierRight1":
                originalPosition = barrierRight1OriginalPosition;
                movedPosition = barrierRight1MovedPosition;
                if (hasWaterPassedBarrier1 && !hasWaterRightReachedTankBottom && barrier.transform.localPosition.Equals(barrierRight1OriginalPosition)) Invoke("MoveWaterBelowBarrierRight1", 0.5f);
                break;
            case "BarrierRight2":
                originalPosition = barrierRight2OriginalPosition;
                movedPosition = barrierRight2MovedPosition;
                if (hasWaterPassedBarrierRight1 && !hasWaterRightReachedTankBottom && barrier.transform.localPosition.Equals(barrierRight2OriginalPosition))
                {
                    Invoke("MoveWaterBelowBarrierRight2", 0.5f);
                }
                break;
            case "BarrierLeft1":
                originalPosition = barrierLeft1OriginalPosition;
                movedPosition = barrierLeft1MovedPosition;
                extendBarrierMoveCooldown = true;
                if (hasWaterPassedBarrier1 && !hasWaterPassedBarrierLeft1 && barrier.transform.localPosition.Equals(barrierLeft1OriginalPosition)) Invoke("MoveWaterBelowBarrierLeft1", 0.5f);
                if (hasWaterPassedBarrier1 && hasWaterPassedBarrierLeft1 && !hasWaterLeftReachedTankBottom && !hasWaterGoneLeft && barrier.transform.localPosition.Equals(barrierLeft1MovedPosition))
                {
                    Invoke("StartMovingWaterLeftTankBottom", 0.5f);
                }
                break;
            case "BarrierLeft2":
                originalPosition = barrierLeft2OriginalPosition;
                movedPosition = barrierLeft2MovedPosition;
                if (hasWaterPassedBarrierLeft1 && !hasWaterLeftReachedTankBottom && barrier.transform.localPosition.Equals(barrierLeft2MovedPosition))
                {
                    Invoke("MakeWaterGoLeft", 0.5f);
                }
                break;
        }

        if (barrier.transform.localPosition.Equals(originalPosition))
        {
            barrier.transform.localPosition = movedPosition;
        } else if (barrier.transform.localPosition.Equals(movedPosition))
        {
            barrier.transform.localPosition = originalPosition;
        }

        canMoveBarriers = false;
        if (extendBarrierMoveCooldown) Invoke("EnableBarrierMoving", 1f);
        else Invoke("EnableBarrierMoving", 0.5f);
    }

    private void Update()
    {
        if (hasWaterRightReachedTankBottom && hasWaterLeftReachedTankBottom && !doorIsOpen)
        {
            isTubeSystemCompleted = true;
            Invoke("OpenDoor", 3);
        }
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

    private void OnDestroy()
    {
        Destroy(tankTopRenderer.material);
        Destroy(tubeMiddlePart1Renderer.material);
        Destroy(tubeMiddlePart2Renderer.material);
        Destroy(tubeMiddlePart3Renderer.material);
        Destroy(tubeRightPart1Renderer.material);
        Destroy(tubeRightPart2Renderer.material);
        Destroy(tubeRightPart3Renderer.material);
        Destroy(tubeRightPart4Renderer.material);
        Destroy(tubeRightPart5Renderer.material);
        Destroy(tubeRightPart6Renderer.material);
        Destroy(tubeLeftPart1Renderer.material);
        Destroy(tubeLeftPart2Renderer.material);
        Destroy(tubeLeftPart3Renderer.material);
        Destroy(tubeLeftPart4Renderer.material);
        Destroy(tubeLeftPart5Renderer.material);
        Destroy(tubeLeftPart6Renderer.material);
        Destroy(tubeLeftRightPart1Renderer.material);
        Destroy(tubeLeftRightPart2Renderer.material);
        Destroy(tubeLeftRightPart3Renderer.material);
        Destroy(tubeLeftLeftPart1Renderer.material);
        Destroy(tubeLeftLeftPart2Renderer.material);
        Destroy(tubeLeftLeftPart3Renderer.material);
        Destroy(tankBottomPart2Renderer.material);
        Destroy(tankBottomPart1Renderer.material);
    }
}
