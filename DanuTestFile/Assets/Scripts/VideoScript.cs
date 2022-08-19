using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;

public class VideoScript : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    public AudioMixerSnapshot isWatching;
    public AudioMixerSnapshot notWatching;
    

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

            isWatching.TransitionTo(.7f);
        }
        
     } 

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Outside sphere");
            videoPlayer.Pause();

            notWatching.TransitionTo(.7f);
        }
    }

}
