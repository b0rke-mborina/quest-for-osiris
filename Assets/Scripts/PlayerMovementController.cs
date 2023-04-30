using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    CharacterController controller;

    Vector3 move;
    Vector3 input;

    float speed;
    public float runSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        GroundedMovement();
        controller.Move(move * Time.deltaTime);
    }

    void HandleInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        input = transform.TransformDirection(input);
        input = Vector3.ClampMagnitude(input, 1f);
    }

    void GroundedMovement()
    {
        speed = runSpeed;
        move.x = input.x != 0 ? input.x * speed : 0;
        move.z = input.z != 0 ? input.z * speed : 0;
        move = Vector3.ClampMagnitude(move, speed);
    }
}
