using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;
    public float moveSpeed;

    [Header("Keybinds")]
   // public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;

    Rigidbody rb;

    Vector3 moveDirection;

    public MovementState state;
    public enum MovementState
    {
        crouching,
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            
        }


        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }


        if (Input.GetKey(crouchKey))
        {
            rb.AddForce(moveDirection.normalized * crouchSpeed * 10f, ForceMode.Force);

        }
    }

    private void StateHandler()
    {

        if (Input.GetKey(crouchKey))
        {
           if (state == MovementState.crouching)
            {
                moveSpeed = 2f;
            }
            
        }
    }
}
