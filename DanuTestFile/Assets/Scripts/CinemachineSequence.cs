using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSequence : MonoBehaviour
{
    public CinemachineVirtualCamera vCamera1;
    public CinemachineVirtualCamera vCamera2;
    public CinemachineVirtualCamera vCameraFP;

    
    // Start is called before the first frame update
    void Start()
    {
        vCamera1.m_Priority = 2;
        vCamera2.m_Priority = 1;
        vCameraFP.m_Priority = 0;
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
