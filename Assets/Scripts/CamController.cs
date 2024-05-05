using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


/*
The purpose of this script is to switch between our two different Cinemachine cameras when the player
flips gravity. Which cinemachine camera is active depends on its current priority, so the this script
swaps active cameras by changing their priorities
*/
public class CamController : MonoBehaviour
{
    public CinemachineFreeLook unflippedCam;
    public CinemachineFreeLook flippedCam;
    public CinemachineFreeLook startCam;
    private CinemachineFreeLook currentCam;
    void Start()
    {
        currentCam = startCam;

        // Set the correct priorities based on the selected startCam
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
