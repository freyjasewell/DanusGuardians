using Assets.Audio.Audio_Scripts.MixerManagement;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    [SerializeField] private MixerTransitionManager mixerTransitionManager;

    //[SerializeField] private GameObject mixerManagerGO;


    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        //mixerTransitionManager = mixerManagerGO.GetComponent<MixerTransitionManager>();
        mixerTransitionManager = FindObjectOfType<MixerTransitionManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            videoPlayer.Play();

            mixerTransitionManager.IsWatchingVideoAudioTransition();

        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //videoPlayer.Pause();
            videoPlayer.Stop();


            // Music Check
            mixerTransitionManager.MusicCheckTransition();
        }
    }


}
