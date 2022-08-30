using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource playMusic;
    private Collider m_Collider;

    private void OnTriggerEnter(Collider other)
    {
        print("Collision");

        m_Collider = GetComponent<Collider>();


        if (!playMusic.isPlaying)
        {
            playMusic.Play();
            m_Collider.enabled = !m_Collider.enabled;
        }
        
    }
}
