using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{

    public Transform target;

    private bool teleport;
 

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        rb = other.gameObject.GetComponent<Rigidbody>();
        teleport = false;
        other.gameObject.transform.position = target.position;
        teleport = true;
    }

}
