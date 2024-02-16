using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float dashSpeed;

    public float groundDrag;

    [SerializeField] private TrailRenderer tr;

    private bool ActivateAnimation = false;
    private bool isMovingForward = false;
    private bool isMovingBackwards = false;

    public Transform cam;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float jumpForce;
    public float jumpForceDown;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

   

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Crouching")]
    public float crouchSpeed;
   /* public float crouchYScale;
    private float startYScale;*/

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode sprintKey = KeyCode.LeftShift;

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



    Animator m_Animator;
    public Animator animator;

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
       // crouching,
    }

    public bool dashing;

    public bool wallrunning;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Tr = GetComponent<TrailRenderer>();
        rb.freezeRotation = true;
        m_Animator = FindObjectOfType<Animator>();
        readyToJump = true;
        tr.emitting = false;
        m_Animator.SetBool("IsJumping", false);
        isJumping = false;
        m_Animator.SetBool("IsFalling", false);
       // startYScale = transform.localScale.y;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
        {
            m_Animator.SetBool("IsGrounded", true);
            isGrounded = true;
            

            rb.drag = groundDrag;
            m_Animator.SetBool("IsJumping", false);
            isJumping = false;
            m_Animator.SetBool("IsFalling", false);
            
        }

        else 
        { 
            rb.drag = 0;

            m_Animator.SetBool("IsGrounded", false);
            isGrounded = false;
           

            if((isJumping && rb.velocity.y < 2) || rb.velocity.y < -2)
            {
                m_Animator.SetBool("IsFalling", true);
               // m_Animator.SetBool("IsIdle", false);

            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dasher && !isGrounded)
        {
             StartCoroutine(Dash());
        }

       // Jump();
        if (grounded == true && Input.GetButtonDown("Jump")) // Den här kollar om spelaren är på marken om den är så ska man kunna man kunna trycka på space för att hoppa.
        {
            print("ground jump");
            //isJumping = true;
            //jumpTimeCounter = jumpTime;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            exitingSlope = true;
            m_Animator.SetBool("IsJumping", true);
            isJumping = true;
            // rb.AddForce(velocity = Vector3.up * jumpHeight);
            // rb.AddForce(transform.up * jumpHeight);
        }
      
        
    }

    

    private void FixedUpdate()
    {
        MovePlayer();

        if(!grounded && !Input.GetButton("Jump"))
        {
            rb.AddForce(-transform.up * jumpForceDown);
            exitingSlope = false;
            m_Animator.SetBool("IsJumping", true);
            isJumping = true;
        }
     
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        // m_Animator.transform.localPosition = Vector3.zero;
        //m_Animator.transform.localEulerAngles = Vector3.zero;
       // grounded &&

        if (Input.GetKey(Forward))
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
           // m_Animator.SetBool("IsFalling", true);

        }

        if (Input.GetKey(Forward))
        {
            m_Animator.SetFloat("Sprint", moveSpeed);
        }

        else if (Input.GetKey(Left))
        {
            m_Animator.SetFloat("Sprint", moveSpeed);
        }

        else if (Input.GetKey(Right))
        {
            m_Animator.SetFloat("Sprint", moveSpeed);
        }

        else if (Input.GetKey(Backward))
        {
            m_Animator.SetFloat("Sprint", moveSpeed);
        }
        else
        {
            m_Animator.SetFloat("Sprint", 0);
            // m_Animator.SetBool("IsFalling", true);

        }

        if (Input.GetKeyDown(crouchKey))
        {
            // rb.AddForce(moveDirection.normalized * crouchSpeed, ForceMode.Force);
            moveSpeed = crouchSpeed;
            m_Animator.SetBool("IsCrouching", true);
        }


        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            moveSpeed = 12f;
            m_Animator.SetBool("IsCrouching", false);
        }

        if (Input.GetKeyDown(sprintKey))
        {
            // rb.AddForce(moveDirection.normalized * crouchSpeed, ForceMode.Force);
            moveSpeed = sprintSpeed;
        }


        // stop crouch
        if (Input.GetKeyUp(sprintKey))
        {
            moveSpeed = 12f;
        }

        // when to jump
        /*  if (Input.GetButtonDown("Jump") && readyToJump && grounded)
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
        // start crouch
        /* if (Input.GetKeyDown(crouchKey))
         {
             transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
             rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
         }

         // stop crouch
         if (Input.GetKeyUp(crouchKey))
         {
             transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
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

      //  Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        /*if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(30f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //(moveDir.normalized * speed * Time.deltaTime);
            // calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        }
        else if (direction.magnitude >= 0f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);




        }*/

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
       /* if (grounded == true && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }*/

            

        //m_Animator.SetTrigger("Jump");

         if (grounded == true && Input.GetButtonDown("Jump")) // Den här kollar om spelaren är på marken om den är så ska man kunna man kunna trycka på space för att hoppa.
         {
             print("ground jump");
             isJumping = true;
             jumpTimeCounter = jumpTime;
             rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

           // rb.AddForce(velocity = Vector3.up * jumpHeight);
            // rb.AddForce(transform.up * jumpHeight);
        }

         if (Input.GetButton("Jump") && isJumping == true) /* Den här koden ser till så att när spelaren trycker på space och inte håller ner space
                                                                så blir det ett kortare hopp och den ser också till så att det inte funkar i luften.*/
         {


             if (jumpTimeCounter > 0)
             {
                 print("continue jump");
                // rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

                 //rb.AddForce(velocity = Vector3.up * jumpHeight);

               // velocity = Vector3.up * jumpHeight;

                jumpTimeCounter -= Time.deltaTime;
             }
             else
             {
                 print("nej jump");
                 isJumping = false;
             }
         }
         else if (grounded == false && (Input.GetButton("Jump") == false || isJumping == false))
         {
             isJumping = false;
            //rb.AddForce(-transform.up * jumpForce, ForceMode.Impulse);
          //  rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }


    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }
    private void StateHandler()
    {
       /* if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }*/

        if (dasher)
        {
            state = MovementState.dashing; 
            moveSpeed = dashSpeed;
        }

        // Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
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