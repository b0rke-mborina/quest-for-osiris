using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // controller and variables for checking
    CharacterController controller;
    public Transform groundChecker;
    public LayerMask groundMask;
    public LayerMask wallMask;

    // movement main variables
    Vector3 move;
    Vector3 input;
    Vector3 verticalVelocity;
	Vector3 forwardDirection;

    // speeds
    float speed;
    public float runSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float airSpeed;
    public float climbSpeed;

    // speed changes
    public float slideSpeedIncrease;
    public float slideSpeedDecrease;
    public float wallrunSpeedIncrease;
    public float wallrunSpeedDecrease;

    // stores available jumps
    int numberOfJumpCharges;
    
    // movement states
	public bool isGrounded;
    bool isCrouching;
    bool isSprinting;
	bool isSliding;
    bool isWallrunning;
    bool isWalljumping;
    bool isClimbing;

    // gravities
    float gravity;
    public float normalGravity;
    public float wallrunGravity;

    // player heights
    public float jumpHeight;
    public float standHeight;
    public float crouchHeight = 0.5f;

    // player centers
    Vector3 standCenter = new Vector3(0, 0, 0);
    Vector3 crouchCenter = new Vector3(0, 0.5f, 0);

    // timers
    float slideTimer;
    public float maxSlideTimer;
    float walljumpTimer;
    public float maxWalljumpTimer;
    float climbTimer;
    public float maxClimbTimer;

    // wallrunning related variables
    bool hasWallrun;
    bool onLeftWall;
    bool onRightWall;
    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    Vector3 wallNormal;
    Vector3 lastWallNormal;

    // climbing related variables
    bool canClimb;
    bool hasClimbed;
    RaycastHit wallHit;

    // camera related variables
    public Camera playerCamera;
    float normalFOV;
    public float specialFOV;
    public float cameraChangeTime;
    public float wallrunTilt;
    public float tilt;


    // MAIN FUNCTIONS (START and UPDATE)

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        standHeight = transform.localScale.y;
        normalFOV = playerCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        CheckWallrun();
        CheckClimbing();

        if (isGrounded && !isSliding)
        {
            GroundedMovement();
        }
        else if (!isGrounded && !isWallrunning && !isClimbing)
        {
            AirMovement();
        }
        else if (isSliding)
        {
            SlideMovement();
            DecreaseSpeed(slideSpeedDecrease);
            slideTimer -= 1f * Time.deltaTime;
            if (slideTimer < 0)
            {
                isSliding = false;
            }
        }
        else if (isWallrunning)
        {
            WallrunMovement();
            DecreaseSpeed(wallrunSpeedDecrease);
        }
        else if (isClimbing)
        {
            ClimbMovement();
            climbTimer -= 1f * Time.deltaTime;
            if (climbTimer < 0)
            {
                isClimbing = false;
                hasClimbed = true;
            }
        }

        CheckGround();
        controller.Move(move * Time.deltaTime);
        ApplyGravity();
        CameraEffects();
    }


    // INPUT

    void HandleInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        input = transform.TransformDirection(input);
        input = Vector3.ClampMagnitude(input, 1f);

        if (Input.GetKeyDown(KeyCode.Space) && numberOfJumpCharges > 0)
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


    // MOVEMENTS

    void GroundedMovement()
    {
        speed = isSprinting
            ? sprintSpeed
            : isCrouching
                ? crouchSpeed
                : runSpeed;
        forwardDirection = Vector3.zero;
        move.x = input.x != 0 ? move.x + input.x * speed : 0;
        move.z = input.z != 0 ? move.z + input.z * speed : 0;
        move = Vector3.ClampMagnitude(move, speed);
    }

    void AirMovement()
    {
        move.x += input.x * airSpeed;
        move.z += input.z * airSpeed;
        if (isWalljumping)
        {
            move += forwardDirection * airSpeed;
            walljumpTimer -= 1f * Time.deltaTime;
            if (walljumpTimer <= 0f)
            {
                isWalljumping = false;
            }
        }
        move = Vector3.ClampMagnitude(move, speed);
    }

    void SlideMovement()
    {
        forwardDirection = Vector3.zero;
        move += forwardDirection;
        move = Vector3.ClampMagnitude(move, speed);
    }

    void WallrunMovement()
    {
        if (input.z > (forwardDirection.z - 10f) && input.z < (forwardDirection.z + 10f)) move += forwardDirection;
        else if (input.z < (forwardDirection.z - 10f) && input.z > (forwardDirection.z + 10f))
        {
            move.x = 0f;
            move.z = 0f;
            StopWallrunning();
        }

        move.x += input.x * airSpeed;
        move = Vector3.ClampMagnitude(move, speed);
    }

    void ClimbMovement()
    {
        forwardDirection = Vector3.up;
        move.x += input.x * airSpeed;
        move.z += input.z * airSpeed;

        verticalVelocity += forwardDirection;
        speed = climbSpeed;

        move = Vector3.ClampMagnitude(move, speed);
        verticalVelocity = Vector3.ClampMagnitude(verticalVelocity, speed);
    }


    // ACTIONS
    
    void Jump()
    {
        if (!isGrounded && !isWallrunning) numberOfJumpCharges -= 1;
        else if (isWallrunning)
        {
            StopWallrunning();
            IncreaseSpeed(wallrunSpeedIncrease);
        }

        hasClimbed = false;
        climbTimer = maxClimbTimer;
        verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * normalGravity);
    }

    void Wallrun()
    {
        isWallrunning = true;
        numberOfJumpCharges = 1;
        IncreaseSpeed(wallrunSpeedIncrease);
        verticalVelocity = new Vector3(0f, 0f, 0f);
        forwardDirection = Vector3.Cross(wallNormal, Vector3.up);
        if (Vector3.Dot(forwardDirection, transform.forward) < 0) forwardDirection = -forwardDirection;
    }

    void StopWallrunning()
    {
        isWallrunning = false;
        lastWallNormal = wallNormal;
        forwardDirection = wallNormal;
        isWalljumping = true;
        walljumpTimer = maxWalljumpTimer;
    }

    void Crouch()
    {
        controller.height = crouchHeight;
        controller.center = crouchCenter;
        transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
        isCrouching = true;

        if (speed > runSpeed)
        {
            isSliding = true;
            forwardDirection = transform.forward;
            if (isGrounded)
            {
                IncreaseSpeed(slideSpeedIncrease);
            }
            slideTimer = maxSlideTimer;
        }
    }
    void Stand()
    {
        controller.height = standHeight * 2;
        controller.center = standCenter;
        transform.localScale = new Vector3(transform.localScale.x, standHeight, transform.localScale.z);
        isCrouching = false;
        isSliding = false;
    }


    // SPEED

    void IncreaseSpeed(float speedIncrease)
    {
        speed += speedIncrease;
    }

    void DecreaseSpeed(float speedDecrease)
    {
        speed -= speedDecrease * Time.deltaTime;
    }
    

    // CHECKS

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, 0.2f, groundMask);
        if (isGrounded)
        {
            numberOfJumpCharges = 1;
            hasWallrun = false;
            hasClimbed = false;
            climbTimer = maxClimbTimer;
        }
    }

    void CheckWallrun()
    {
        onLeftWall = Physics.Raycast(transform.position, -transform.right, out leftWallHit, 0.7f, wallMask);
        onRightWall = Physics.Raycast(transform.position, transform.right, out rightWallHit, 0.7f, wallMask);

        if (isGrounded) StopWallrunning();
        else if ((onRightWall || onLeftWall) && !isWallrunning) TestWallrun();
        else if ((!onRightWall && !onLeftWall) && isWallrunning) StopWallrunning();
    }

    void TestWallrun()
    {
        wallNormal = onRightWall ? rightWallHit.normal : leftWallHit.normal;
        if (hasWallrun)
        {
            float wallAngle = Vector3.Angle(wallNormal, lastWallNormal);
            if (wallAngle > 15) Wallrun();
        }
        else
        {
            Wallrun();
            hasWallrun = true;
        }
    }

    void CheckClimbing()
    {
        canClimb = Physics.Raycast(transform.position, transform.forward, out wallHit, 0.7f, wallMask);
        float wallAngle = Vector3.Angle(-wallHit.normal, transform.forward);
        if (wallAngle < 15 && !hasClimbed && canClimb) isClimbing = true;
        else isClimbing = false;
    }

    
    // GRAVITY

    void ApplyGravity()
    {
        gravity = isWallrunning
            ? wallrunGravity
            : isClimbing
                ? 0f
                : normalGravity;
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }


    // CAMERA

    void CameraEffects()
    {
        float fov = isWallrunning
            ? specialFOV
            : isSliding
                ? specialFOV
                : normalFOV;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, fov, cameraChangeTime * Time.deltaTime);

        if (isWallrunning)
        {
            if (onRightWall) tilt = Mathf.Lerp(tilt, wallrunTilt, cameraChangeTime * Time.deltaTime);
            if (onLeftWall) tilt = Mathf.Lerp(tilt, -wallrunTilt, cameraChangeTime * Time.deltaTime);
        }
        else tilt = Mathf.Lerp(tilt, 0f, cameraChangeTime * Time.deltaTime);
    }
}
