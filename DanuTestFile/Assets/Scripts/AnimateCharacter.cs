using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimateCharacter : MonoBehaviour
{
    private Vector3[] targetDest = new Vector3[targetIndex];
    public static int targetIndex;
    private static int randomIndex;

    public Transform[] positions;

    private NavMeshAgent agent;

    Animator anim;

    private bool isWaving = false;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.autoBraking = false;

        anim.SetBool("IsWalking", true);

        Randomize();
        // SetPath();
    }

    public void FixedUpdate()
    {
        Movement();
        StartCoroutine("Wave2");
    }

    //Previous pathway positions
    void Movement()
    {
        agent.destination = positions[randomIndex].position;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            Randomize();

        FaceTarget();


        //transform.LookAt(agent.destination);


       

        /* To traverse through array
         * agent.destination = targetDest[targetIndex];
         * 
       targetIndex = (targetIndex++) % targetDest.Length;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            targetIndex++;

          if (targetDest.Length == targetIndex)
         {
             targetIndex = 0;
         }
        
         transform.LookAt(targetDest[targetIndex]); */

        // private float speed = 1.2f;

        // transform.position = Vector3.MoveTowards(transform.position, targetDest[targetIndex], speed);

        // anim.SetFloat("Speed", speed); //Would like to have the states based on speed float.
    }


    void Randomize()
    {
        randomIndex = Random.Range(0, positions.Length);
    }

    void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //Trying to get Wave annimation to work on Keydown
    void Wave()
    {

        //Wave animation on Space down - currently sets it permanently to true. But it causes it to bounce between states.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("WaveTrigger");

            isWaving = true;

            if (isWaving == true)
            {
                agent.isStopped = true;
                
            }
        }

        //Need this to be a timer/coroutine or on isWaving == false;
        if (Input.GetKeyUp(KeyCode.Space))
        {
            agent.isStopped = false;
        }

    }

    IEnumerator Wave2()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("WaveTrigger");

            agent.isStopped = true;
            yield return new WaitForSeconds(3.5f);
            agent.isStopped = false;
        }
    }

    void SetPath()
    {
        //Set the vectors to be the points you want it to visit on the Level
        targetDest[0] = new Vector3(21f, 15.4f, 0f);
        targetDest[1] = new Vector3(12f, 15.15f, 6);
        targetDest[2] = new Vector3(21f, 15.5f, 13f);
        targetDest[3] = new Vector3(18.7f, 16.4f, -6.6f);
    }


}
