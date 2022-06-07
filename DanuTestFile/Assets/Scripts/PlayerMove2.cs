using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove2 : MonoBehaviour
    

{
    public CharacterController controller;

    public float speed = 0.3f;


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

        //Move the player


        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        //transform.Translate
        agent.Move(dir * agent.speed * Time.deltaTime);

        //Horizontal doesn't rotate... can't move forward in any other direction.
    }
}

