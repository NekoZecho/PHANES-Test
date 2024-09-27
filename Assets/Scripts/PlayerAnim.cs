using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
   
    void Start()
    {
        
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        GetComponent<Animator>().SetFloat("x", xInput);
        GetComponent <Animator>().SetFloat("y", yInput);
        if (GetComponent<PlayerMovement>().isRunning == true)
        {
            bool running = true;
            GetComponent<Animator>().SetBool("run", running);
        }
        else
        {
            bool running = false;
            GetComponent<Animator>().SetBool("run", running);
        }
    }
}
