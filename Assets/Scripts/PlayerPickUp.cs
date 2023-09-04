using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject holdObj;
    private Rigidbody holdObjRB;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickUpForce = 150.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(holdObj == null)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }
        if (holdObj != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(holdObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - holdObj.transform.position);
            holdObjRB.AddForce(moveDirection * pickUpForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.CompareTag("Chest"))
        {
            return;
        }

        if (pickObj.GetComponent<Rigidbody>())
        {
            holdObjRB = pickObj.GetComponent<Rigidbody>();
            holdObjRB.useGravity = false;
            holdObjRB.drag = 10;
            holdObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            holdObjRB.transform.parent = holdArea;
            holdObj = pickObj;
        }
    }

    void DropObject()
    {
        holdObjRB.useGravity = true;
        holdObjRB.drag = 1;
        holdObjRB.constraints = RigidbodyConstraints.None;

        holdObj.transform.parent = null;
        holdObj = null;
    }
}