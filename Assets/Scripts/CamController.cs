using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public CinemachineFreeLook unflippedCam;
    public CinemachineFreeLook flippedCam;
    public CinemachineFreeLook startCam;
    private CinemachineFreeLook currentCam;
    void Start()
    {
        currentCam = startCam;
        setCamPriorities();
    }

    private void setCamPriorities() {
        if (unflippedCam == currentCam) {
            unflippedCam.Priority = 20;
            flippedCam.Priority = 10;
        }
        else {
            unflippedCam.Priority = 10;
            flippedCam.Priority = 20;
        }
    }

    public void swapCams() {
        if (currentCam == unflippedCam)
            currentCam = flippedCam;
        else
            currentCam = unflippedCam;
        setCamPriorities();
    }
}
