using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LjusStamina : MonoBehaviour
{
    public float Stamina = 100;
    public float MaxStamina = 100f;

    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Stamina = Stamina -= 1 * Time.deltaTime * 5;
            print("Working");
        }


    }
}