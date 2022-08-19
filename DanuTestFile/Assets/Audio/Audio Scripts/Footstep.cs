using UnityEngine.Audio;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] hitClips;
    private AudioClip[] splashClips;

    [Range(.1f,.5f)]
    public float volumeChangeMultiply = .2f;
    [Range(.1f,.5f)]
    public float pitchChangeMultiply = .35f;


    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

  
    //Play Clip Method
    void Step()
    {
        AudioClip hitclip = GetRandomHitClip();

        audioSource.volume = Random.Range(.5f - volumeChangeMultiply, .85f);
        audioSource.pitch = Random.Range(1 - pitchChangeMultiply, 1 + pitchChangeMultiply);
        audioSource.PlayOneShot(hitclip);


        

        //Debug.Log("Hit");

    }

    private AudioClip GetRandomHitClip()
    {
        return hitClips[UnityEngine.Random.Range(0, hitClips.Length)];
    }




}
