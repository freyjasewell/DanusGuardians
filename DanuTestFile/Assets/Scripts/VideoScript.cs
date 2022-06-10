using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void OnTriggerEnter(Collider other)
     {
        if(other.tag == "Player")
        {
            Debug.Log("In the sphere");
            videoPlayer.Play();
        }
        
     } 

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Outside sphere");
            videoPlayer.Pause();
        }
    }

}
