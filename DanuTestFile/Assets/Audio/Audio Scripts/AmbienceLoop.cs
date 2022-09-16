using UnityEngine;

public class AmbienceLoop : MonoBehaviour
{
    public AudioSource ambienceSource;
    public AudioClip ambienceTail;


    // Start is called before the first frame update
    void Start()
    {
        ambienceSource.PlayOneShot(ambienceTail);
        ambienceSource.PlayScheduled(AudioSettings.dspTime + ambienceTail.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
