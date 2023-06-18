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
    // private float distCovered;
    bool isOnPlatform;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        platform = this.gameObject;
        originalLocalPosition = platform.transform.localPosition;
        lastPosition = "original";

        startTime = Time.time;
        journeyLength = 3.0f; // Vector3.Distance(originalLocalPosition, movedLocalPosition);
        speed = 1.0f;
        // distCovered = 0f;
        isOnPlatform = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") isOnPlatform = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") isOnPlatform = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = Mathf.PingPong(Time.time - startTime, journeyLength / speed);
        float fractionOfJourney = distCovered / journeyLength;
        transform.localPosition = Vector3.Lerp(originalLocalPosition, movedLocalPosition, fractionOfJourney);
    }
}
