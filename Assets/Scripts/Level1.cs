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
    public CharacterController playerModel;
    [SerializeField] public float _speed = 5;
    int flipCount = 0;

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
        if(playerModel.isGrounded || ((playerModel.collisionFlags & CollisionFlags.Above) != 0)){
            flipCount = 0;
        }
        if(Input.GetKeyDown("q") && flipCount == 0){
            flipCount += 1;
            flip = !flip;
            playerModel.transform.localEulerAngles = new Vector3(180+ playerModel.transform.localEulerAngles.x, 180 + playerModel.transform.localEulerAngles.y, playerModel.transform.localEulerAngles.z);
            camController.swapCams();
        }
    }
}
