using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCharacter : MonoBehaviour
{
    private float speed = 0.03f;
    
    public Vector3[] targetDest = new Vector3[targetIndex];
    public static int targetIndex; //Does this being static stop it from working? Does it need initialising differently?

    public int lastTarget = 3; //targetIndex.Length - 1;

    private void Awake()
    {
        Animator Anim = GetComponent<Animator>();
    }

    private void Start()
    {
        targetIndex = 0;

        targetDest[0] = new Vector3(21f, 15.4f, 0f);

        targetDest[1] = new Vector3(14.8f, 15.15f, 4);

        targetDest[2] = new Vector3(21f, 15.5f, 13f);

        targetDest[3] = new Vector3(18.7f, 16.4f, -6.6f);
    }

    public void FixedUpdate()
    {
        Debug.Log(targetIndex);
        //Move the NPC towards the target

        // Put set destination from Navmesh here:

        transform.LookAt(targetDest[targetIndex]);
        transform.position = Vector3.MoveTowards(transform.position, targetDest[targetIndex], speed);


        // Set Walking animation
        GetComponent<Animator>().SetBool("IsWalking", true);

        if (transform.position == targetDest[targetIndex])
        {
            if(targetIndex == lastTarget)
            {
                //Is there a way to turn this into a circle? Go back to beginning of array?
                targetIndex = 0;
            }
            targetIndex++;

        }

    }

    //Previous pathway positions
    void Pathways()
    {
        targetDest[0] = new Vector3(7.7f, 16.6f, 0.45f);

        targetDest[1] = new Vector3(3, 15.82f, 3);

        targetDest[2] = new Vector3(7.6f, 16f, 7.5f);
    }

    
}
