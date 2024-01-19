using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{

    public Transform target;

    private bool teleport;
 



    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "TP")
        {
            teleport = false;
            other.gameObject.transform.position = target.position;
            teleport = true;
        }
      
    }

}
