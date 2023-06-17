using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlatformingSectionHandler : MonoBehaviour
{
    GameObject player;
    MagicUseHandler magicHandler;

    public GameObject useMagicCanvas;
    public Text useMagicText;
    string labelText = "[F] Use magic";

    bool isShifted;
    bool isInTrigger;

    Transform[] childPlatforms;
    List<Collider> childPlatformColliders;
    List<AlphaChange> childPlatformAlphaChangeScripts;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        magicHandler = player.GetComponent<MagicUseHandler>();

        useMagicCanvas = GameObject.Find("Canvas");
        useMagicText = useMagicCanvas.GetComponentInChildren<Text>();

        isShifted = false;
        isInTrigger = false;

        childPlatforms = GetComponentsInChildren<Transform>().Where(r => r.tag == "Platform").ToArray();
        childPlatformColliders = new List<Collider>();
        childPlatformAlphaChangeScripts = new List<AlphaChange>();

        // make every other platform transparent
        int platformIndex = 0;
        foreach (Transform child in childPlatforms)
        {
            childPlatformColliders.Add(child.GetComponentInChildren<Collider>());
            childPlatformAlphaChangeScripts.Add(child.GetComponentInChildren<AlphaChange>());

            if (platformIndex % 2 != 0)
            {
                childPlatformColliders[platformIndex].enabled = !childPlatformColliders[platformIndex].enabled;
                childPlatformAlphaChangeScripts[platformIndex].MakeTransparent();
            }

            platformIndex++;
        }
    }

    private void Update()
    {
        if (magicHandler.canItUseMagic() && isInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            isShifted = !isShifted;
            ChangePlatforms();
            magicHandler.setMagicUse(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        magicHandler.setMagicUse(true);
        isInTrigger = true;
        useMagicText.text = labelText;
    }

    private void OnTriggerExit(Collider other)
    {
        magicHandler.setMagicUse(false);
        isInTrigger = false;
        useMagicText.text = "";
    }

    private void ChangePlatforms()
    {
        for (int i = 0; i < childPlatforms.Length; i++)
        {
            childPlatformColliders[i].enabled = !childPlatformColliders[i].enabled;
            if ((!isShifted && i % 2 != 0) || (isShifted && i % 2 == 0))
            {
                childPlatformAlphaChangeScripts[i].MakeTransparent();
            }
            else
            {
                childPlatformAlphaChangeScripts[i].MakeOpaque();
            }
        }
    }
}
