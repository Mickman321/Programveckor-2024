using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finishpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        print("blahblahkbla");
        if (collision.CompareTag("Player"))
        {
            print("ok");
            SceneController.instance.NextLevel1();
        }
    }
}
