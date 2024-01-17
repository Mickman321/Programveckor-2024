using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPasscode : MonoBehaviour
{
    public Behaviour PasscodeCanvas;
    public float detectionRange;
    public bool closeEnough;
    public Transform player;


    private void Start()
    {
        detectionRange = 5f;
        PasscodeCanvas.enabled = !PasscodeCanvas.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        closeEnough = false;
        if (Vector3.Distance(player.position, transform.position) <= detectionRange)
        {
            closeEnough = true;



            if (closeEnough && Input.GetKeyUp(KeyCode.Tab))
            {
                PasscodeCanvas.enabled = !PasscodeCanvas.enabled;
            }

        }
    }
}


