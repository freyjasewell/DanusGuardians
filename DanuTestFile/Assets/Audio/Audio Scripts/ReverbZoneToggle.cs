using UnityEngine;

public class ReverbZoneToggle : MonoBehaviour
{

    public GameObject reverbZone;
    public AudioReverbZone theZone;



    private void Awake()
    {
        theZone = reverbZone.GetComponent<AudioReverbZone>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (theZone.enabled)
            {
                theZone.enabled = !theZone.enabled;

            }

            else if (!theZone.enabled)
            {
                theZone.enabled = true;

            }

        }
    }

}
