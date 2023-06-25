using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4Puzzle2MechanismHandler : MonoBehaviour
{
    GameObject mechanism;
    GameObject puzzleTask;

    GameObject key;
    GameObject buttonUp;
    GameObject buttonDown;

    GameObject lockMoveable1;
    GameObject lockMoveable2;
    GameObject lockMoveable3;
    GameObject lockMoveable4;
    GameObject lockMoveable5;

    int lock1MaxPosition = 1;
    int lock1MinPosition = -3;
    int lock2MaxPosition = 3;
    int lock2MinPosition = -2;
    int lock3MaxPosition = 4;
    int lock3MinPosition = -1;
    int lock4MaxPosition = 3;
    int lock4MinPosition = -1;
    int lock5MaxPosition = 3;
    int lock5MinPosition = -3;

    int currentLockIndex;
    GameObject currentLock;
    int currentLockPosition;

    GameObject planeSignMessage;

    Vector3 moveUp;
    Vector3 moveDown;

    Vector3 keyOriginalPosition;
    Vector3 moveKey;

    Vector3 planeSignMessageOriginalPosition;
    Vector3 planeSignMessageMovedPosition;

    // prompt related variables
    public GameObject interactPromptCanvas;
    public Text interactPromptText;
    string labelText = "[LMB] Press";

    // player camera direction registration variables
    public GameObject mainCamera;
    public LayerMask interactableMask;
    
    public bool isInRangeOfMechanismButtons;
    bool canPressButton;
    public bool isMechanismCompleted;

    // Start is called before the first frame update
    void Start()
    {
        mechanism = this.gameObject;
        puzzleTask = mechanism.transform.parent.gameObject;

        key = transform.Find("Key").gameObject;
        buttonUp = transform.Find("ButtonUp").gameObject;
        buttonDown = transform.Find("ButtonDown").gameObject;

        lockMoveable1 = transform.Find("MoveablePart1").gameObject;
        lockMoveable2 = transform.Find("MoveablePart2").gameObject;
        lockMoveable3 = transform.Find("MoveablePart3").gameObject;
        lockMoveable4 = transform.Find("MoveablePart4").gameObject;
        lockMoveable5 = transform.Find("MoveablePart5").gameObject;

        currentLockIndex = 1;
        currentLock = lockMoveable1; // lockMoveable1;
        currentLockPosition = 0;

        planeSignMessage = transform.Find("SignMessage").gameObject;

        moveUp = Vector3.up * 0.5f;
        moveDown = Vector3.down * 0.5f;

        keyOriginalPosition = key.transform.localPosition;
        moveKey = Vector3.right * 1.8f;

        planeSignMessageOriginalPosition = planeSignMessage.transform.localPosition;
        planeSignMessageMovedPosition = new Vector3(8.09f, 1.305f, -1.664f);

        interactPromptCanvas = GameObject.Find("Canvas");
        interactPromptText = interactPromptCanvas.GetComponentInChildren<Text>();

        mainCamera = GameObject.Find("Main Camera");

        isInRangeOfMechanismButtons = false;
        canPressButton = true;
        isMechanismCompleted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isInRangeOfMechanismButtons = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRangeOfMechanismButtons = false;
        interactPromptText.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && isInRangeOfMechanismButtons && canPressButton && !isMechanismCompleted)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.name == "ButtonUp")
                {
                    interactPromptText.text = labelText;

                    // interaction with LMB
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        HandleButtonUpPress();
                    }
                } else if (hit.collider.gameObject.name == "ButtonDown")
                {
                    interactPromptText.text = labelText;

                    // interaction with LMB
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        HandleButtonDownPress();
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
    private void HandleButtonUpPress()
    {
        canPressButton = false;
        if (CanLockBeMoved(currentLockIndex, "up"))
        {
            currentLock.transform.localPosition += moveUp;
            currentLockPosition += 1;
        }
        if (CanKeyBeMoved(currentLockIndex, currentLockPosition)) MoveKey();
        Invoke("EnableButtonPress", 0.5f);
    }

    // handle interaction
    private void HandleButtonDownPress()
    {
        canPressButton = false;
        if (CanLockBeMoved(currentLockIndex, "down"))
        {
            currentLock.transform.localPosition += moveDown;
            currentLockPosition -= 1;
        }
        if (CanKeyBeMoved(currentLockIndex, currentLockPosition)) MoveKey();
        Invoke("EnableButtonPress", 0.5f);
    }

    private void EnableButtonPress()
    {
        canPressButton = true;
    }

    private bool CanLockBeMoved(int indexOfLock, string direction)
    {
        int maxPosition = 0;
        int minPosition = 0;


        switch (indexOfLock)
        {
            case 1:
                maxPosition = lock1MaxPosition;
                minPosition = lock1MinPosition;
                break;
            case 2:
                maxPosition = lock2MaxPosition;
                minPosition = lock2MinPosition;
                break;
            case 3:
                maxPosition = lock3MaxPosition;
                minPosition = lock3MinPosition;
                break;
            case 4:
                maxPosition = lock4MaxPosition;
                minPosition = lock4MinPosition;
                break;
            case 5:
                maxPosition = lock5MaxPosition;
                minPosition = lock5MinPosition;
                break;
        }

        if (direction.Equals("up"))
        {
            if (currentLockPosition < maxPosition) return true;
            else return false;
        }
        else
        {
            if (currentLockPosition > minPosition) return true;
            else return false;
        }
    }

    private bool CanKeyBeMoved(int indexOfLock, int positionOfLock)
    {
        switch (indexOfLock)
        {
            case 1:
                if (positionOfLock == 1) return true;
                else return false;
            case 2:
                if (positionOfLock == 2 || positionOfLock == 3 || positionOfLock == -2) return true;
                else return false;
            case 3:
                if (positionOfLock == -1 || positionOfLock == 3 || positionOfLock == 4) return true;
                else return false;
            case 4:
                if (positionOfLock == 3) return true;
                else return false;
            case 5:
                if (positionOfLock == 1 || positionOfLock == 2 || positionOfLock == 3 || positionOfLock == -2 || positionOfLock == -3) return true;
                else return false;
            default:
                return false;
        }
    }

    private void MoveKey()
    {
        key.transform.localPosition += moveKey;
        if (currentLockIndex < 5)
        {
            currentLockIndex += 1;
            switch (currentLockIndex)
            {
                case 1:
                    currentLock = lockMoveable1;
                    break;
                case 2:
                    currentLock = lockMoveable2;
                    break;
                case 3:
                    currentLock = lockMoveable3;
                    break;
                case 4:
                    currentLock = lockMoveable4;
                    break;
                case 5:
                    currentLock = lockMoveable5;
                    break;
            }
            currentLockPosition = 0;
        }
        else
        {
            isMechanismCompleted = true;
            interactPromptText.text = "";
            planeSignMessage.transform.localPosition = planeSignMessageMovedPosition;
        }
    }
}
