using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    Transform[] childPlatforms;
    List<Collider[]> childPlatformColliders;
    List<AlphaChange[]> childPlatformAlphaChangeScripts;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        magicHandler = player.GetComponent<MagicUseHandler>();

        useMagicCanvas = GameObject.Find("Canvas");
        useMagicText = useMagicCanvas.GetComponentInChildren<Text>();

        isShifted = false;

        // make every other platform transparent
        childPlatforms = GetComponentsInChildren<Transform>().Where(r => r.tag == "Platform").ToArray();
        childPlatformColliders = new List<Collider[]>();
        childPlatformAlphaChangeScripts = new List<AlphaChange[]>();
        int platformIndex = 0;
        foreach (Transform child in childPlatforms)
        {
            if (child.gameObject.tag == "Platform")
            {
                // save childrens' colliders
                childPlatformColliders.Add(child.GetComponentsInChildren<Collider>());
                childPlatformAlphaChangeScripts.Add(child.GetComponentsInChildren<AlphaChange>());

                if (platformIndex % 2 != 0)
                {
                    foreach (Collider col in childPlatformColliders[platformIndex])
                    {
                        col.enabled = !col.enabled;
                    }
                    foreach (AlphaChange script in childPlatformAlphaChangeScripts[platformIndex])
                    {
                        script.MakeTransparent();
                    }
                }

                platformIndex++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        magicHandler.setMagicUse(true);
        useMagicText.text = labelText;
    }

    private void OnTriggerExit(Collider other)
    {
        magicHandler.setMagicUse(false);
        useMagicText.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && magicHandler.canItUseMagic())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isShifted = !isShifted;
                ChangePlatforms();
                magicHandler.setMagicUse(true);
            }
        }
    }

    private void ChangePlatforms()
    {
        for (int i = 0; i < childPlatforms.Length; i++)
        {
            foreach (Collider col in childPlatformColliders[i])
            {
                col.enabled = !col.enabled;
            }
            foreach (AlphaChange script in childPlatformAlphaChangeScripts[i])
            {
                if ((!isShifted && i % 2 != 0) || (isShifted && i % 2 == 0))
                {
                    script.MakeTransparent();
                }
                else
                {
                    script.MakeOpaque();
                }
            }
        }
    }
}
