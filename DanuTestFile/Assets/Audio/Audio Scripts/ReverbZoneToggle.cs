using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class ReverbZoneToggle : MonoBehaviour
{
    [Header("Reverb Zone Toggle")]
    public AudioReverbZone reverbZone;

    
    [Header("Music Trigger Script Location")]
    [Space(30)]
    [SerializeField] private GameObject musicPlayer;
    private MusicTrigger musicTriggerScript;

    [Header("Snapshot Transisitons")]
    [Space(10)]
    [SerializeField] private AudioMixerSnapshot inPlantSnapMasterMixer;
    [SerializeField] private AudioMixerSnapshot outPlantSnapMasterMixer;
    [SerializeField] private AudioMixerSnapshot musicPlayingSnapshot;
    [SerializeField] private AudioMixerSnapshot inPlantSnapNPCMixer;
    [SerializeField] private AudioMixerSnapshot outPlantSnapNPCMixer;
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


            if (PlayerInPlant == true) //Snapshot Management
            {
                PlayerInPlant = !PlayerInPlant;

                if (musicTriggerScript.isPlaying == true)
                {
                    musicPlayingSnapshot.TransitionTo(musicTriggerScript.transitionTimeOn);

                    outPlantSnapNPCMixer.TransitionTo(musicTriggerScript.transitionTimeOff); //NPC Mixer

                    //Debug.Log("OutPlant & Music");

                }
                else if (musicTriggerScript.isPlaying == false)
                {
                    outPlantSnapMasterMixer.TransitionTo(musicTriggerScript.transitionTimeOff);
                    outPlantSnapNPCMixer.TransitionTo(musicTriggerScript.transitionTimeOff); //NPC Mixer
                    
                    //Debug.Log("OutPlant");
                }

            }
            else if (PlayerInPlant == false)
            {
                PlayerInPlant = true;

                inPlantSnapMasterMixer.TransitionTo(musicTriggerScript.transitionTimeOn);
                inPlantSnapNPCMixer.TransitionTo(musicTriggerScript.transitionTimeOn); //NPC Mixer

                //Debug.Log("InPlant");
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
