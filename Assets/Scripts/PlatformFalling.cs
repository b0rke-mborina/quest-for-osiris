using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFalling : MonoBehaviour
{
    GameObject platform;
    Rigidbody body;
    bool isSteppedOn;

    // Start is called before the first frame update
    void Start()
    {
        platform = this.gameObject;
        body = GetComponent<Rigidbody>();
        isSteppedOn = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GroundChecker" && !isSteppedOn)
        {
            Debug.Log("OK");
            isSteppedOn = true;
            Invoke("setGravityTrue", 2);
        }
    }

    void setGravityTrue()
    {
        body.useGravity = true;
    }
}
