using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool isFalling = false;
    float downSpeed = 0;

    public GameObject Player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
            isFalling = true;
    }


    void Update()
    {
        if (isFalling)
        {
            downSpeed += Time.deltaTime/100;
            transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed, transform.position.z);
        }
    }
}
