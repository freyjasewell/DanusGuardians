using UnityEngine;
using UnityEngine.AI;

public class NPCMovementScript : MonoBehaviour
{
    // Public Variables
    [Header("Game Objects")]
    public GameObject player;

    [Header("Other Variables")]
    public float waitTimeBetweenPoints;

    [Space]

    [Header("Destination Positions")]
    public Transform[] positions;



    // Private Variables
    int randomIndex;
    float waitTime;
    bool facingPlayer = false;
    NavMeshAgent agent;
    Animator anim;

    [Space (10)]

    [Header("Voice Manager")]
    [SerializeField] DanuVoiceLines danuVoiceScript;


    private void Start()
    {
        // References Setup
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();




        waitTime = waitTimeBetweenPoints;

        Randomize();

        //Ollie's Audio Script
        danuVoiceScript = gameObject.GetComponent<DanuVoiceLines>();


    }

    public void FixedUpdate()
    {
        // Check if wanting to face player
        if (facingPlayer == true)
        {
            FacePlayer();
        }
        else if (facingPlayer == false)
        {
            FaceForward();
        }

        Movement();
    }

    // NPC Moves to new randomised point
    void Movement()
    {

        agent.SetDestination(positions[randomIndex].position);
        if (agent.remainingDistance > 0.5f)
        {
            anim.SetBool("IsWalking", true);
        }
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            anim.SetBool("IsWalking", false);
            if (waitTime <= 0)
            {
                WaitForNextDestination();
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }
    }

    // Randomise Index for list of Destinations
    void Randomize()
    {
        randomIndex = Random.Range(0, positions.Length - 1);
    }

    // Stops and Waits for next destination
    void WaitForNextDestination()
    {
        Randomize();
        anim.SetBool("IsWalking", true);
        waitTime = waitTimeBetweenPoints;
    }

    // NPC Looks where they are going
    void FaceForward()
    {
        Vector3 direction = (agent.steeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // NPC Looks at Player
    void FacePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // NPC Sphere Collider - NPC stops walking, faces player and waves.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            anim.SetTrigger("WaveTrigger");
            facingPlayer = true;
            agent.isStopped = true;

            //Ollie's Voice Script Method

            danuVoiceScript.Introduction();

        }
    }

    //Restarts the NPC walking
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("WaveFinished");

            facingPlayer = false;
            agent.isStopped = false;
        }
    }


}
