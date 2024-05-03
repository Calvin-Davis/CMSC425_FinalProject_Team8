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
            StartCoroutine(SmoothRotateCoroutine());
            camController.swapCams();
        }
    }

    private IEnumerator SmoothRotateCoroutine()
    {
        float elapsedTime = 0.0f;

        Vector3 startRotation = CC.transform.GetChild(3).localEulerAngles;
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
