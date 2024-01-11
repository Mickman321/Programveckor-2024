using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{

    LjusStamina staminaScript;
    private void Update()
    {

        staminaScript = GetComponentInParent<LjusStamina>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ljus"))
        {
            staminaScript.Stamina = 100f;
        }
        
    }
}
