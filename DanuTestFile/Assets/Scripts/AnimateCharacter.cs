using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimateCharacter : MonoBehaviour
{
    public Vector3[] targetDest = new Vector3[targetIndex];
    public static int targetIndex;
    private static int randomIndex; 

    private NavMeshAgent agent;
   // private NavMeshPath pathway = new NavMeshPath();
   //Would a NavMeshPath be a better way to get the NPC to move around?

    Animator anim;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.autoBraking = false;

      //  NavMesh.CalculatePath(transform.position, NavMesh.AllAreas, int areaMask??, pathway);

        anim.SetBool("IsWalking", true);


        SetPath();
    }

    public void FixedUpdate()
    {
        Movement();

        // agent.SetPath(pathway);

        //Wave animation on Space down - currently sets it permanently to true. But it causes it to bounce between states.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetBool("IsWaving", true);

            //Attempted to implement IENumerator to get it working
            // Wave();
        }

    }

    //Previous pathway positions
    void Movement()
    {
       agent.destination = targetDest[targetIndex];
        
       targetIndex = (targetIndex++) % targetDest.Length;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            targetIndex++;
        
        if (targetDest.Length == targetIndex)
        {
            targetIndex = 0;
        } 

        transform.LookAt(targetDest[targetIndex]);


        // private float speed = 1.2f;

        // transform.position = Vector3.MoveTowards(transform.position, targetDest[targetIndex], speed);

        // anim.SetFloat("Speed", speed); //Would like to have the states based on speed float.
    }


    void SetPath()
    {
        randomIndex = Random.Range(0, targetDest.Length); //Attempt to get random.range working 

        //Set the vectors to be the points you want it to visit on the Level
        targetDest[0] = new Vector3(21f, 15.4f, 0f);

        targetDest[1] = new Vector3(12f, 15.15f, 6);

        targetDest[2] = new Vector3(21f, 15.5f, 13f);

        targetDest[3] = new Vector3(18.7f, 16.4f, -6.6f);
    }


    //Trying to get Wave annimation to work on Keydown
    IEnumerator Wave()
    {

        GetComponent<Animator>().SetBool("IsWaving", true);


        /* if (Input.GetKeyUp(KeyCode.Space))
             GetComponent<Animator>().SetBool("IsWaving", false);*/

        yield return new WaitForSeconds(0.5f);

        //Stop Movement?

    }
}
