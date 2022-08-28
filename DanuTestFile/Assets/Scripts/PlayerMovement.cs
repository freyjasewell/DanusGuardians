using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    //Variable set up
    public CharacterController controller;
    private Animator anim;
    private NavMeshAgent agent;

   // public float speed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        Animation();
    }
    
    void MovePlayer()
    {
        //Move the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        agent.Move(move * agent.speed / 15);
    }

    void Animation()
    {

        //Animation variables
        bool movingFoward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool movingBackwards = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

        //Walking Forwards
        if (movingFoward == true)
        {
            anim.SetBool("IsWalking", true);
        }
        if (movingFoward == false)
        {
            anim.SetBool("IsWalking", false);
        }

        //Walking backwards
        if (movingBackwards == true)
        {
            anim.SetBool("Backwards", true);
        }
        if (movingBackwards == false)
        {
            anim.SetBool("Backwards", false);
        }

        //Side step animation
        if(moveRight == true)
        {
            anim.SetBool("RightStep", true);
        }
        if (moveRight == false)
        {
            anim.SetBool("RightStep", false);
        }

        if (moveLeft == true)
        {
            anim.SetBool("LeftStep", true);
        }
        if (moveLeft == false)
        {
            anim.SetBool("LeftStep", false);

        }
    }

}
