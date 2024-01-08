using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float dashSpeed;

    public float groundDrag;

    [SerializeField] private TrailRenderer tr;

    private bool ActivateAnimation = false;
    private bool isMovingForward = false;
    private bool isMovingBackwards = false;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [SerializeField]
    KeyCode Forward;

    [SerializeField]
    KeyCode Left;

    [SerializeField]
    KeyCode Right;

    [SerializeField]
    KeyCode Backward;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public Vector3 velocity;

    public bool isGrounded;
    [SerializeField]
    private float jumpTimeCounter;
    [SerializeField]
    public float jumpTime;
    private bool isJumping;
    public float jumpHeight = 10f;

    public float wallrunSpeed;

    private bool dasher = true;
    private float dashingPower = 60f; 
    private float dashingTime = 0.2f;
    private float dashingCooldown = 5f;



    //Animator m_Animator;
    //public Animator animator;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        wallrunning,
        climbing,
        dashing,
        air,
    }

    public bool dashing;

    public bool wallrunning;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Tr = GetComponent<TrailRenderer>();
        rb.freezeRotation = true;
        /*m_Animator = FindObjectOfType<Animator>();*/
        readyToJump = true;
        tr.emitting = false;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;

        else
            rb.drag = 0;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dasher && !isGrounded)
        {
             StartCoroutine(Dash());
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // m_Animator.transform.localPosition = Vector3.zero;
        //m_Animator.transform.localEulerAngles = Vector3.zero;


        /*if (Input.GetKey(Forward))
        {
            m_Animator.SetFloat("Run", moveSpeed);
        }

        else if (Input.GetKey(Left))
        {
            m_Animator.SetFloat("Run", moveSpeed);
        }

        else if (Input.GetKey(Right))
        {
            m_Animator.SetFloat("Run", moveSpeed);
        }

        else if (Input.GetKey(Backward))
        {
            m_Animator.SetFloat("Run", moveSpeed);
        }
        else
        {
            m_Animator.SetFloat("Run", 0);
        }
        */

        // when to jump
        if (Input.GetButtonDown("Jump") && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        /* if (grounded == true && Input.GetButtonDown("Jump")) // Den här kollar om spelaren är på marken om den är så ska man kunna man kunna trycka på space för att hoppa.
         {
             print("ground jump");
             isJumping = true;
             jumpTimeCounter = jumpTime;
             rb.velocity = Vector3.up * jumpForce;
         }

         if (Input.GetButton("Jump") && isJumping == true) /* Den här koden ser till så att när spelaren trycker på space och inte håller ner space
                                                                så blir det ett kortare hopp och den ser också till så att det inte funkar i luften.*/
        /* {


             if (jumpTimeCounter > 0)
             {
                 print("continue jump");
                 rb.velocity = Vector3.up * jumpForce;
                 jumpTimeCounter -= Time.deltaTime;
             }
             else
             {
                 print("nej jump");
                 isJumping = false;
             }
         }
         else
         {
             isJumping = false;
         }*/
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 12f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
       else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        // turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        //m_Animator.SetTrigger("Jump");

        /* if (grounded == true && Input.GetButtonDown("Jump")) // Den här kollar om spelaren är på marken om den är så ska man kunna man kunna trycka på space för att hoppa.
         {
             print("ground jump");
             isJumping = true;
             jumpTimeCounter = jumpTime;
             rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
         }

         if (Input.GetButton("Jump") && isJumping == true) /* Den här koden ser till så att när spelaren trycker på space och inte håller ner space
                                                                så blir det ett kortare hopp och den ser också till så att det inte funkar i luften.*/
        /* {


             if (jumpTimeCounter > 0)
             {
                 print("continue jump");
                 rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                 jumpTimeCounter -= Time.deltaTime;
             }
             else
             {
                 print("nej jump");
                 isJumping = false;
             }
         }
         else
         {
             isJumping = false;
         }*/
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }
    private void StateHandler()
    {
        if (dasher)
        {
            state = MovementState.dashing; 
            moveSpeed = dashSpeed;
        }

        // Mode - wallrunning
        else if (wallrunning)
        {
            state = MovementState.wallrunning;
            moveSpeed = wallrunSpeed;
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    private IEnumerator Dash()
    {
        dasher = true;
        tr.emitting = true;
       // velocity = new Vector3(transform.forward.x * dashingPower, 0f, transform.forward.z * dashingPower);
        yield return new WaitForSeconds(dashingTime);
        velocity = Vector3.zero;
        tr.emitting = false;
        yield return new WaitForSeconds(dashingCooldown); 
        dasher = true;

    }
}