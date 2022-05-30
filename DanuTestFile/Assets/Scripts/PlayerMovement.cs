using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 3f;
    public float gravity = -9.8f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    bool isGrounded;


    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    
    void Movement()
    {
        //Is player touching the ground
     /*   isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if ((isGrounded == false) && velocity.y < 0f)
        {
            velocity.y = -2f;
        } */

        //Move the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        //controller.Move(move * speed * Time.deltaTime);

        agent.Move(move);
    }
}
