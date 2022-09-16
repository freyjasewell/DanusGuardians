using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;

public class VideoScript : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    public AudioMixerSnapshot isWatching;
    public AudioMixerSnapshot notWatchingMusicOff;
    public AudioMixerSnapshot notWatchingMusicON;

    public float audioFadeInTime = 1.5f;
    public float audioFadeOutTime = 1.7f;

    public GameObject musicTrigger;
    

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }


    private void OnTriggerEnter(Collider other)
     {
        if(other.tag == "Player")
        {
            videoPlayer.Play();

            isWatching.TransitionTo(audioFadeInTime);
        }
        
     } 

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //videoPlayer.Pause();
            videoPlayer.Stop();

            if (musicTrigger.gameObject == null)
            {
                Debug.Log("MusicOff");
                notWatchingMusicOff.TransitionTo(audioFadeOutTime);
            }
            
            else if (musicTrigger)
            {
                Debug.Log("MusicOn");
                notWatchingMusicON.TransitionTo(audioFadeOutTime);
            }
        }
    }

}
