using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroydoor : MonoBehaviour
{
    CheckPassword checkpassword;
    // Start is called before the first frame update
    void Start()
    {
        checkpassword = FindObjectOfType<CheckPassword>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        

        if(collision.gameObject.tag == "Player")
        {
            if (checkpassword.door == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
