using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpikeRaycast : MonoBehaviour
{
    [SerializeField] private int rayLenght = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName= null;
    [SerializeField] private GameObject interactUI;

    private ButtonSpikeController raycastedObj;

    [SerializeField] private KeyCode lowerSpikeKey= KeyCode.Mouse0;
    [SerializeField] private RawImage Crosshair= null;
    private bool isCrosshairActive;
    private bool doOnce;


    private const string interactableTag= "SpikeButton";


    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;
        if(Physics.Raycast(transform.position, fwd, out hit, rayLenght, mask))
        {
            if(hit.collider.CompareTag(interactableTag))
            {
                if(!doOnce)
                {
                    raycastedObj= hit.collider.gameObject.GetComponent<ButtonSpikeController>();
                    CrosshairChange(true);

                }

                isCrosshairActive = true;
                doOnce = false;
          

                if(Input.GetKeyDown(lowerSpikeKey)) 
                {
                    raycastedObj.PlayAnimation();
                }
            }
            interactUI.SetActive(true);
        }

        else
        {
            interactUI.SetActive(false);
            if (isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce = false;
            }
        }
    }
    void CrosshairChange(bool on)
    {
        if(on && !doOnce)
        {
            Crosshair.color = Color.red;
        }
        else
        {
            Crosshair.color = Color.black;
            isCrosshairActive= false;
        }
    }
}
