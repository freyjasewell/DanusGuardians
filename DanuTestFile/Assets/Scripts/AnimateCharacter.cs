using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCharacter : MonoBehaviour
{
    private float speed = 0.06f;
    
    public Vector3[] targetDest = new Vector3[targetIndex];
    public static int targetIndex; //Does this being static stop it from working? Does it need initialising differently?

    //public Vector3 targetDestV = new Vector3(3, 15.82f, 3);
    //public GameObject targetDest; Box collider / OnTriggerEnter didn't work

    //public int pivotPoint;
    // pivotPoint++;

    private void Start()
    {
        targetIndex = 0;

        targetDest[0] = new Vector3(7.7f, 16.6f, 0.45f);

        targetDest[1] = new Vector3(3, 15.82f, 3);

        targetDest[2] = new Vector3(7.6f, 16f, 7.5f);
    }

    public void FixedUpdate()
    {
        Debug.Log(targetIndex);
        //Move the NPC towards the target
        transform.LookAt(targetDest[targetIndex]);
        transform.position = Vector3.MoveTowards(transform.position, targetDest[targetIndex], speed);

        if (transform.position == targetDest[targetIndex])
        {
            targetIndex++;
        }

    }

    void Pathways()
    {
        targetDest[0] = new Vector3(7.7f, 16.6f, 0.45f);

        targetDest[1] = new Vector3(3, 15.82f, 3);

        targetDest[2] = new Vector3(7.6f, 16f, 7.5f);
    }

    
}
