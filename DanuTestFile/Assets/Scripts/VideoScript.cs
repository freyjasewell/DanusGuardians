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

    public float audioFadeInTime = 1.5f;
    public float audioFadeOutTime = 1.7f;
    

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

            isWatching.TransitionTo(audioFadeInTime);
        }
        
     } 

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Outside sphere");
            videoPlayer.Pause();

            notWatching.TransitionTo(audioFadeOutTime);
        }
    }

}
