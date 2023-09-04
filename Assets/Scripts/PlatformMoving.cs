using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    GameObject player;
    GameObject platform;

    Vector3 originalLocalPosition;
    public Vector3 movedLocalPosition;
    string lastPosition;

    private float startTime;
    private float journeyLength;
    private float speed;

    PlayerMovementController playerMovementController;
    bool isOnPlatform;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovementController = player.GetComponent<PlayerMovementController>();

        platform = this.gameObject;
        originalLocalPosition = platform.transform.localPosition;
        lastPosition = "original";

        startTime = Time.time;
        journeyLength = 3.0f;
        speed = 1.0f;
        isOnPlatform = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GroundChecker")
        {
            isOnPlatform = true;
            // player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "GroundChecker")
        {
            isOnPlatform = false;
            // player.transform.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = Mathf.PingPong(Time.time - startTime, journeyLength / speed);
        float fractionOfJourney = distCovered / journeyLength;
        transform.localPosition = Vector3.Lerp(originalLocalPosition, movedLocalPosition, fractionOfJourney);

        // Debug.Log(isOnPlatform);

        if (isOnPlatform && playerMovementController.isGrounded)
        {
            // Debug.Log(player.transform.localPosition);
            // player.transform.localPosition = Vector3.Lerp(originalLocalPosition, movedLocalPosition, fractionOfJourney);
        }
    }
}
