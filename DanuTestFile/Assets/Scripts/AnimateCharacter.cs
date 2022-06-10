using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimateCharacter : MonoBehaviour
{
    public Transform[] positions;
    private static int randomIndex;

    private NavMeshAgent agent;
    Animator anim;
    public GameObject player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.autoBraking = false;

        anim.SetBool("IsWalking", true);

        Randomize();
    }

    public void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        agent.destination = positions[randomIndex].position;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            Randomize();

        FaceTarget(agent.destination); //Overides the FaceDirection in the Wave function because this is in an update.

    }

    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("WaveTrigger");
            
            anim.SetBool("IsWalking", false);
            agent.isStopped = true;

            //StartCoroutine("Wave");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //anim.SetBool("IsWaving", false);
            anim.SetBool("IsWalking", true);

            agent.isStopped = false;
        }
    }

    IEnumerator Wave()
    {
        FaceTarget(player.transform.position); //Gets overidden by Movement in FixedUpdate... how do I pause that?
        //transform.LookAt(player.transform.position); //It quickly goes back to agent.destination

        GetComponent<Animator>().SetTrigger("WaveTrigger");

        agent.isStopped = true;
       //Could FaceDirection be paused or changed here?
        yield return new WaitForSeconds(3.55f);
        agent.isStopped = false;
    }

    void Randomize()
    {
        randomIndex = Random.Range(0, positions.Length);
    }

}
