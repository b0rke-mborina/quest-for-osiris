using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Level3Puzzle2Handler : MonoBehaviour
{
    GameObject laserTurret1;
    GameObject laserTurret2;
    GameObject laserTurret3;
    GameObject laserTurret4;

    GameObject laserTurret1Canon;
    GameObject laserTurret2Canon;
    GameObject laserTurret3Canon;
    GameObject laserTurret4Canon;

    GameObject laserMark1;
    GameObject laserMark2;
    GameObject laserMark3;
    GameObject laserMark4;

    GameObject pyramid;

    Quaternion laserTurret1OriginalRotation;
    Quaternion laserTurret1RotatedRotation;

    Vector3 laserMark1ActivatedPosition;
    Vector3 laserMark1DeactivatedPosition;
    Vector3 laserMark2ActivatedPosition;
    Vector3 laserMark2DeactivatedPosition;
    Vector3 laserMark3ActivatedPosition;
    Vector3 laserMark3DeactivatedPosition;
    Vector3 laserMark4ActivatedPosition;
    Vector3 laserMark4DeactivatedPosition;

    Vector3 pyramidOriginalPosition;
    Vector3 pyramidMovedPosition;

    // prompt related variables
    public GameObject interactPromptCanvas;
    public Text interactPromptText;
    public GameObject mainCamera;

    string labelTextPress = "[LMB] Press";
    string labelTextRotate = "[LMB] Rotate";
    string labelTextUseMagic = "[LMB] Use magic";

    bool canInteractWithPuzzleElements;
    bool isCompleted;

    // Start is called before the first frame update
    void Start()
    {
        laserTurret1 = transform.Find("LaserTurret1").gameObject;
        laserTurret2 = transform.Find("LaserTurret2").gameObject;
        laserTurret3 = transform.Find("LaserTurret3").gameObject;
        laserTurret4 = transform.Find("LaserTurret4").gameObject;

        laserTurret1Canon = transform.Find("LaserTurret1/Canon").gameObject;
        laserTurret2Canon = transform.Find("LaserTurret2/Canon").gameObject;
        laserTurret3Canon = transform.Find("LaserTurret3/Canon").gameObject;
        laserTurret4Canon = transform.Find("LaserTurret4/Canon").gameObject;

        laserMark1 = transform.Find("LaserMark1").GetChild(0).gameObject;
        laserMark2 = transform.Find("LaserMark2").GetChild(0).gameObject;
        laserMark3 = transform.Find("LaserMark3").GetChild(0).gameObject;
        laserMark4 = transform.Find("LaserMark4").GetChild(0).gameObject;

        pyramid = transform.Find("Pyramid").gameObject;

        laserTurret1OriginalRotation = laserTurret1.transform.localRotation;
        laserTurret1RotatedRotation = Quaternion.Euler(0f, 0f, -90f);

        laserMark1ActivatedPosition = laserMark1.transform.localPosition;
        laserMark1DeactivatedPosition = new Vector3(0f, 1.780536f, -0.43f);
        laserMark2ActivatedPosition = laserMark2.transform.localPosition;
        laserMark2DeactivatedPosition = new Vector3(0f, 1.780536f, -0.43f);
        laserMark3ActivatedPosition = laserMark3.transform.localPosition;
        laserMark3DeactivatedPosition = new Vector3(0f, 1.780536f, -0.43f);
        laserMark4ActivatedPosition = new Vector3(0f, 1.780536f, -0.43f);
        laserMark4DeactivatedPosition = laserMark4.transform.localPosition;

        pyramidOriginalPosition = pyramid.transform.localPosition;
        pyramidMovedPosition = new Vector3(-10.928f, -12.65f, 1.3197f); ;

        interactPromptCanvas = GameObject.Find("Canvas");
        interactPromptText = interactPromptCanvas.GetComponentInChildren<Text>();
        mainCamera = GameObject.Find("Main Camera");

        canInteractWithPuzzleElements = false;
        isCompleted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        canInteractWithPuzzleElements = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canInteractWithPuzzleElements = false;
        interactPromptText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // RaycastHit hit;
        // Physics.Raycast(laserTurret3Canon.transform.position, laserTurret3Canon.transform.right, out hit, Mathf.Infinity);

        Ray ray = new Ray(laserTurret3Canon.transform.position, laserTurret3Canon.transform.up);
        Debug.DrawRay(ray.origin, ray.direction * 10);

        // Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        // Debug.DrawRay(transform.position, forward, Color.green);
    }
}
