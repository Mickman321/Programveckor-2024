using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private TheMove pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;

   /* [Header("CameraEffects")]
    public PlayerCam cam;
    public float dashFov;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVel = true;*/

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;
    private bool wPressed;
    private bool leftshiftPressed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<TheMove>();
    }

    private void Update()
    {
         if (Input.GetKeyDown(dashKey))
         {
            Dash();
         }
          if (dashCdTimer > 0)
             dashCdTimer -= Time.deltaTime;

        /* if (Input.GetKey(dashKey) || Input.GetKey("w"))
         {
             Dash();
         }

         else if (Input.GetKey(dashKey) || Input.GetKey("d"))
         {
             Dash2();
         }
         //  Dash();

        */
        /* if (Input.GetKey(dashKey))
         {
             if (Input.GetKey(KeyCode.W))
             {
                 Dash();
             }

             else if (Input.GetKey(KeyCode.D))
             {
                 Dash2();
             }
         }*/

        /*  if (Input.GetKeyDown(KeyCode.W))
          {
              if (leftshiftPressed)
              {
                  Debug.Log("Both pressed");
                  pm.dashing = true;

              }
              else
              {
                  Debug.Log("W pressed");
                  pm.dashing = false;
              }

              wPressed = true;
          }
          if (Input.GetKeyDown(KeyCode.LeftShift))
          {
              if (wPressed)
              {
                  Debug.Log("Both pressed");
                  pm.dashing = true;
              }
              else
              {
                  Debug.Log("LeftShift pressed");
                  pm.dashing = false;
              }

              leftshiftPressed = true;
          }

          if (Input.GetKeyUp(KeyCode.W))
          {
              wPressed = false;
          }

          if (Input.GetKeyUp(KeyCode.LeftShift))
          {
              leftshiftPressed = false;
              Dash();
          }*/
    }

    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        pm.dashing = true;
       // pm.maxYSpeed = maxDashYSpeed;

       // cam.DoFov(dashFov);

        Transform forwardT;

        /* if (useCameraForward)
             forwardT = playerCam; /// where you're looking
         else
             forwardT = orientation; */ /// where you're facing (no up or down)

        // Vector3 direction = GetDirection(forwardT);

        // Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;

        //new Vector3(rb.velocity.x, 0f, rb.velocity.z);
      /*  Vector3 tempVelocity = rb.velocity.normalized;
        tempVelocity.y = 0;
        rb.AddForce(tempVelocity * dashForce, ForceMode.Impulse);    */

            rb.velocity = orientation.forward * dashForce + orientation.up * dashUpwardForce;
        
        

        //  if (disableGravity)
        //   rb.useGravity = false;

        delayedForceToApply = rb.velocity;

        //delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private void Dash2()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        pm.dashing = true;
        // pm.maxYSpeed = maxDashYSpeed;

        // cam.DoFov(dashFov);

        Transform forwardT;

        /* if (useCameraForward)
             forwardT = playerCam; /// where you're looking
         else
             forwardT = orientation; */ /// where you're facing (no up or down)

        // Vector3 direction = GetDirection(forwardT);

        // Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;
        
            
        
           rb.velocity = orientation.right * dashForce + orientation.up * dashUpwardForce;
        


        //  if (disableGravity)
        //   rb.useGravity = false;

        delayedForceToApply = rb.velocity;

        //delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private void Dash3()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        pm.dashing = true;
        // pm.maxYSpeed = maxDashYSpeed;

        // cam.DoFov(dashFov);

        Transform forwardT;

        /* if (useCameraForward)
             forwardT = playerCam; /// where you're looking
         else
             forwardT = orientation; */ /// where you're facing (no up or down)

        // Vector3 direction = GetDirection(forwardT);

        // Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;
        if (Input.GetKey(dashKey))
        {
            rb.velocity = orientation.forward * dashForce + orientation.up * dashUpwardForce;
        }


        //  if (disableGravity)
        //   rb.useGravity = false;

        delayedForceToApply = rb.velocity;

        //delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private void Dash4()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        pm.dashing = true;
        // pm.maxYSpeed = maxDashYSpeed;

        // cam.DoFov(dashFov);

        Transform forwardT;

        /* if (useCameraForward)
             forwardT = playerCam; /// where you're looking
         else
             forwardT = orientation; */ /// where you're facing (no up or down)

        // Vector3 direction = GetDirection(forwardT);

        // Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;
        if (Input.GetKey(dashKey))
        {
            rb.velocity = orientation.forward * dashForce + orientation.up * dashUpwardForce;
        }


        //  if (disableGravity)
        //   rb.useGravity = false;

        delayedForceToApply = rb.velocity;

        //delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForceToApply;
    private void DelayedDashForce()
    {
       // if (resetVel)
         //   rb.velocity = Vector3.zero;

        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        pm.dashing = false;
      //  pm.maxYSpeed = 0;

       // cam.DoFov(85f);

       // if (disableGravity)
       //     rb.useGravity = true;
    }

   /* private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;

        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }*/
}