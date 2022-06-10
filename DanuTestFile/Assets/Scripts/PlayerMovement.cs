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

    public float speed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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

        agent.Move(move * agent.speed);
    }

    void Animation()
    {
        //Walking Forwards
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("IsWalking", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("IsWalking", false);
        }

        //Walking backwards
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("IsWalking", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("IsWalking", false);
        }
    }
}
