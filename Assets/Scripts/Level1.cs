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
    public float flipRotationTime = 0.5F;
    [SerializeField] public float _speed = 5;
    int flipCount = 0;

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
        //reset flip count if player hits ground. flipCount used to keep player
        //from flipping more than once in the air as infinite flips in air would
        //allow player to fly. Making flipCount an int instead of bool allows
        //possible later implementation of levels where you can flip a couple of
        //times in the air before needing to reset the flip count.
        if(CC.isGrounded || ((CC.collisionFlags & CollisionFlags.Above) != 0)){
            flipCount = 0;
        }

        //flip if allowed when player presses q. Play the flip sound, do the flip
        //animation, and swap our curent active camera.
        if(Input.GetKeyDown("q") && flipCount == 0){
            flipsound.PlayDelayed(0);
            flipCount += 1;
            flip = !flip;
            StartCoroutine(SmoothRotateCoroutine());
            camController.swapCams();
        }
    }

    //used to smoothly flip player model over when gravity is flipped
    private IEnumerator SmoothRotateCoroutine()
    {
        float elapsedTime = 0.0f;

        Vector3 startRotation = CC.transform.GetChild(3).localEulerAngles; // initial orientation of character model
        Vector3 targetRotation = startRotation + new Vector3(180, 180, 0);

        while (elapsedTime < flipRotationTime)
        {
            // Interpolate rotation smoothly over time
            CC.transform.GetChild(3).localEulerAngles = Vector3.Lerp(startRotation, targetRotation, elapsedTime / flipRotationTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
