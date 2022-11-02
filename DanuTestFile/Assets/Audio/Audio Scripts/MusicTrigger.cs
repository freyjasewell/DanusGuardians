using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource playMusic;
    private Collider m_Collider;


    public AudioMixerSnapshot musicOnSnapshot;
    public AudioMixerSnapshot musicOffSnapshot;

    public AudioMixerSnapshot playerNPCDefault;

    public float transitionTimeOn = 1.5f;
    public float transitionTimeOff = 2.5f;
    [HideInInspector] public bool isPlaying = false;
    // public bool reverbZoneToggleEngaged = false;

    AudioSource musicSource;



    private void OnTriggerEnter(Collider other)
    {

        
        musicSource = GetComponent<AudioSource>();

        m_Collider = GetComponent<Collider>(); //Getting Collider to toggle Off






        if (other.tag == "Player")
        {
            isPlaying = true;
            if (!playMusic.isPlaying)
            {
                playMusic.Play();
                musicOnSnapshot.TransitionTo(transitionTimeOn);

                Invoke("musicFinished", musicSource.clip.length);

                m_Collider.enabled = !m_Collider.enabled;
            }
        }


    }
    void musicFinished()
    {
        //Debug.Log("Audio Finished");
        musicOffSnapshot.TransitionTo(transitionTimeOff);

        isPlaying = false;

        Destroy(musicSource);
        Destroy(gameObject);
    }




}
