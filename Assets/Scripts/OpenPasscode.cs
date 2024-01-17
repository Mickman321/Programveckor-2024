using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPasscode : MonoBehaviour
{
    public Behaviour PasscodeCanvas;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PasscodeCanvas.enabled = !PasscodeCanvas.enabled;
        }

    }
}


