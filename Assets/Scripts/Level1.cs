using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    public string nameOfNextLevel;

    public bool flip = false;
    public Transform playerModel;
    [SerializeField] public float _speed = 5;

    // Add reference to our CamController script
    public CamController camController;

    public bool getFlip() {
        return flip;
    }
    public float getSpeed() {
        return _speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("q")){
            flip = !flip;
            playerModel.localEulerAngles = new Vector3(180+ playerModel.localEulerAngles.x, 180 + playerModel.localEulerAngles.y, playerModel.localEulerAngles.z);
            camController.swapCams();
        }
    }
}
