using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Audio;


public class ReverbZoneToggle : MonoBehaviour
{

    public AudioReverbZone reverbZone;

    [Space(30)]
    [SerializeField] private GameObject musicPlayer;
    private MusicTrigger musicTriggerScript;


    [Space(10)]
    [SerializeField] private AudioMixerSnapshot inPlantSnap;
    [SerializeField] private AudioMixerSnapshot outPlantSnap;
    [SerializeField] private AudioMixerSnapshot musicPlayingSnapshot;

    public static bool PlayerInPlant { get; set; }




    



    private void Awake()
    {

        reverbZone = GetComponentInParent<AudioReverbZone>();
        
        musicTriggerScript = musicPlayer.GetComponent<MusicTrigger>(); //Getting Music Trigger Script


        PlayerInPlant = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            Debug.Log(PlayerInPlant);

            Debug.Log("Trigger Hit By Player");

            if (PlayerInPlant == true) //Snapshot Management
            {
                PlayerInPlant = !PlayerInPlant;

                if (musicTriggerScript.isPlaying == true)
                {
                    musicPlayingSnapshot.TransitionTo(musicTriggerScript.transitionTimeOn);

                    Debug.Log("OutPlant & Music");

                }
                else if (musicTriggerScript.isPlaying == false)
                {
                    outPlantSnap.TransitionTo(musicTriggerScript.transitionTimeOff);

                    Debug.Log("OutPlant");
                }

            }
            else if (PlayerInPlant == false)
            {
                PlayerInPlant = true;

                inPlantSnap.TransitionTo(musicTriggerScript.transitionTimeOn);

                Debug.Log("InPlant");
            }

           
            if (reverbZone.enabled) //Reverb Zone Management
            {
                reverbZone.enabled = !reverbZone.enabled;

            }
            else if (!reverbZone.enabled)
            {
                reverbZone.enabled = true;

  

            }



        }
    }

}
