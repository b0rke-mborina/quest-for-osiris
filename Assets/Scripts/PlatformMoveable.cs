using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlatformMoveable : MonoBehaviour
{
    GameObject player;
    MagicUseHandler magicHandler;

    GameObject platform;
    bool isMoved;

    Vector3 defaultPosition;
    public Vector3 movedPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        magicHandler = player.GetComponent<MagicUseHandler>();

        platform = this.gameObject;
        isMoved = false;
        defaultPosition = platform.transform.position;
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
        if (other.gameObject.name == "Player")
        {
            if (!isMoved)
            {
                platform.transform.position = movedPosition;
                isMoved = true;
            }
            else
            {
                platform.transform.position = defaultPosition;
                isMoved = false;
            }
        }
    }
}
