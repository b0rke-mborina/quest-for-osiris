using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicUseHandler : MonoBehaviour
{
    public GameObject canvas;
    public Image imageMagic;
    public GameObject mainCamera;
    bool canUseMagic;

    public Sprite magicDisabled;
    public Sprite magicEnabled;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        canvas = GameObject.Find("Canvas");

        imageMagic = canvas.GetComponentInChildren<Image>();
        imageMagic.sprite = magicDisabled;
        canUseMagic = false;
    }

    // Update is called once per frame
    public void setMagicUse(bool enabled)
    {
        if (enabled)
        {
            canUseMagic = true;
            imageMagic.sprite = magicEnabled;
        }
        else
        {
            canUseMagic = false;
            imageMagic.sprite = magicDisabled;
        }
    }

    public bool getMagicUse()
    {
        return canUseMagic;
    }
}
