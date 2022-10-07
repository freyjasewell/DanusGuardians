using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class CinemachineSequence : MonoBehaviour
{
    public CinemachineVirtualCamera vCamera1;
    public CinemachineVirtualCamera vCamera2;
    public CinemachineVirtualCamera vCameraFP;


    public AudioMixerSnapshot audioSnapMaster_InPlant;
    public AudioMixerSnapshot audioSnapPlayerNPC_InPlant;
    public float audioTransitionTime = 8.5f;



    // Start is called before the first frame update
    void Start()
    {
        vCamera1.m_Priority = 2;
        vCamera2.m_Priority = 1;
        vCameraFP.m_Priority = 0;

        ///Ollies Audio Transitions

        audioSnapMaster_InPlant.TransitionTo(audioTransitionTime);
        audioSnapPlayerNPC_InPlant.TransitionTo(audioTransitionTime);

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("OpeningCamera");
    }

    IEnumerator OpeningCamera()
    {
        //Switches priority of cameras so we only end up using the First Person camera.
        vCamera1.m_Priority = 0;
        yield return new WaitForSeconds(0.5f);
        vCamera2.m_Priority = 0;
        yield return new WaitForSeconds(3.5f);
        vCameraFP.m_Priority = 1;
    }
}
