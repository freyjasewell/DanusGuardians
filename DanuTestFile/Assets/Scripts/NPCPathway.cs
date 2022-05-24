using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPathway : MonoBehaviour
{
    public int pivotPoint;

    
    private void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
       if(other.tag == "NPC")
        {
            pivotPoint++;
        }
            


    }
}
