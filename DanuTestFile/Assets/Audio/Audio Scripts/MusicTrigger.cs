using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Audio;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource playMusic;
    private Collider m_Collider;


    public AudioMixerSnapshot musicOn;
    public AudioMixerSnapshot musicOff;

    public AudioMixerSnapshot playerNPCDefault;

    public float transitionTimeOn = 1.5f;
    public float transitionTimeOff = 2.5f;

    AudioSource musicSource;



    private void OnTriggerEnter(Collider other)
    {
        print("Collision");
        
        musicSource = GetComponent<AudioSource>();

        m_Collider = GetComponent<Collider>();



        if (other.tag == "Player")
        {
            if (!playMusic.isPlaying)
            {
                playMusic.Play();
                musicOn.TransitionTo(transitionTimeOn);

                Invoke("musicFinished", musicSource.clip.length);

                m_Collider.enabled = !m_Collider.enabled;
            }
        }


    }
    void musicFinished()
    {
        Debug.Log("Audio Finished");
        musicOff.TransitionTo(transitionTimeOff);

        Destroy(musicSource);
        Destroy(gameObject);
    }




}
