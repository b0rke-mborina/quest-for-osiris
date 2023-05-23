using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    // camera and variable for pausing camera
    public new Camera camera;
    public bool isPaused;

    // vertical view limits
    public float minX = -60f;
    public float maxX = 60f;

    // main variables for camera controll
    public float sensitivity;
    float rotX = 0f;
    float rotY = 0f;

    // PlayerMovementController instance for tilting
    PlayerMovementController playerMovementController;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerMovementController = GetComponent<PlayerMovementController>();

        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            rotX += Input.GetAxis("Mouse Y") * sensitivity;
            rotY += Input.GetAxis("Mouse X") * sensitivity;

            rotX = Mathf.Clamp(rotX, minX, maxX);

            transform.localEulerAngles = new Vector3(0, rotY, 0);
            camera.transform.localEulerAngles = new Vector3(-rotX, 0, playerMovementController.tilt);
        }
    }
}
