using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlatformMoveable : MonoBehaviour
{
    GameObject player;
    MagicUseHandler magicHandler;

    GameObject platform;
    bool isMoved;

    Vector3 originalLocalPosition;
    public Vector3 movedLocalPosition;

    // prompt related variables
    public GameObject useMagicCanvas;
    public Text useMagicText;
    string labelText = "[F] Use magic";

    // player camera direction registration variables
    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        magicHandler = player.GetComponent<MagicUseHandler>();

        useMagicCanvas = GameObject.Find("Canvas");
        useMagicText = useMagicCanvas.GetComponentInChildren<Text>();

        mainCamera = GameObject.Find("Main Camera");

        platform = this.gameObject;
        isMoved = false;
        originalLocalPosition = platform.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        magicHandler.setMagicUse(true);
    }

    private void OnTriggerExit(Collider other)
    {
        magicHandler.setMagicUse(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && magicHandler.canItUseMagic())
        {
            // detect if player is looking at the interactable object with a Raycast
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "PlatformMoveable")
                {
                    useMagicText.text = labelText;

                    if (Input.GetKey(KeyCode.F))
                    {
                        HandlePlatformMovement();
                        useMagicText.text = "";
                        magicHandler.setMagicUse(true);
                    }
                }
                else
                {
                    useMagicText.text = "";
                }
            }
            else
            {
                useMagicText.text = "";
            }
        }
    }

    private void HandlePlatformMovement()
    {
        if (!isMoved)
        {
            platform.transform.localPosition = movedLocalPosition;
            isMoved = true;
        }
        else
        {
            platform.transform.localPosition = originalLocalPosition;
            isMoved = false;
        }
    }
}
