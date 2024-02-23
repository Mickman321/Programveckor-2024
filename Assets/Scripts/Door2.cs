using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    private bool Isopen = false;
   // private Animator anim;

    Animator m_Animator;
  //  public Animator animator; 

    void Start()
    {
       // anim = GetComponent<Animator>();
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Isopen == true && (Input.GetKeyUp(KeyCode.E)))
        {

            m_Animator.SetTrigger("OpenDoor"); Isopen = false;
        }
    }




    private void OnTriggerEnter(Collider other)
     {
        if (other.tag == "Player")
        {
            Isopen = true; 
        }
     }



    private void OnTriggerExit(Collider other)
    {
        m_Animator.SetTrigger("CloseDoor");
    }
    
        

        
}
