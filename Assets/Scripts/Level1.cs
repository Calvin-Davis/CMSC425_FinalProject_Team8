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
    public CharacterController CC;
    [SerializeField] public float _speed = 5;
    int flipCount = 0;

    // Add reference to our CamController script
    public CamController camController;
    public AudioSource flipsound;
    

    public bool getFlip() {
        return flip;
    }
    public float getSpeed() {
        return _speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(CC.isGrounded || ((CC.collisionFlags & CollisionFlags.Above) != 0)){
            flipCount = 0;

        }
        if(Input.GetKeyDown("q") && flipCount == 0){
            flipsound.PlayDelayed(0);
            flipCount += 1;
            flip = !flip;
            CC.transform.GetChild(3).localEulerAngles = new Vector3(180+ CC.transform.GetChild(3).localEulerAngles.x, 180 + CC.transform.GetChild(3).localEulerAngles.y, CC.transform.GetChild(3).localEulerAngles.z);
            camController.swapCams();
        }
    }
}
