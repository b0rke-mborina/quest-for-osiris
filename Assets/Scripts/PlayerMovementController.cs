using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    CharacterController controller;
    public Transform groundChecker;
    public LayerMask groundMask;

    Vector3 move;
    Vector3 input;
    Vector3 verticalVelocity;

    float speed;
    public float runSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float airSpeed;
    int numberOfJumpCharges;
    bool isGrounded;
    bool isCrouching;
    bool isSprinting;
    float gravity;
    public float normalGravity;
    public float jumpHeight;
    public float standHeight;
    public float crouchHeight = 0.5f;
    Vector3 standCenter = new Vector3(0, 0, 0);
    Vector3 crouchCenter = new Vector3(0, 0.5f, 0);


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        standHeight = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        if (isGrounded) GroundedMovement();
        else AirMovement();
        GroundedMovement();
        CheckGround();
        controller.Move(move * Time.deltaTime);
        ApplyGravity();
    }

    void HandleInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        input = transform.TransformDirection(input);
        input = Vector3.ClampMagnitude(input, 1f);

        if (Input.GetKeyUp(KeyCode.Space) && numberOfJumpCharges > 0)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Stand();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
    }

    void GroundedMovement()
    {
        speed = isSprinting
            ? sprintSpeed
            : isCrouching
                ? crouchSpeed
                : runSpeed;
        move.x = input.x != 0 ? move.x + input.x * speed : 0;
        move.z = input.z != 0 ? move.z + input.z * speed : 0;
        move = Vector3.ClampMagnitude(move, speed);
    }

    void AirMovement()
    {
        move.x += input.x * airSpeed;
        move.z += input.z * airSpeed;
        move = Vector3.ClampMagnitude(move, speed);
    }

    void Jump()
    {
        verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * normalGravity);
        numberOfJumpCharges -= 1;
    }

    void Crouch()
    {
        controller.height = crouchHeight;
        controller.center = crouchCenter;
        transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
        isCrouching = true;
    }

    void Stand()
    {
        controller.height = standHeight * 2;
        controller.center = standCenter;
        transform.localScale = new Vector3(transform.localScale.x, standHeight, transform.localScale.z);
        isCrouching = false;
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, 0.2f, groundMask);
        if (isGrounded)
        {
            numberOfJumpCharges = 1;
        }
    }

    void ApplyGravity()
    {
        gravity = normalGravity;
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }
}
