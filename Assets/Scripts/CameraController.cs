using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 2;
    Vector2 turn;
    public GameObject parent;
    public Transform camera;
    Vector3 initialCameraOrientation;
    Vector3 initialPivotOrientation;
    bool prevFlip;
    float flipDuration = 0.5F;
    
    void Start() { 
        prevFlip = false;
        initialCameraOrientation = camera.localEulerAngles;
        initialPivotOrientation = transform.localEulerAngles;
    }

    void Update()
    {
        //determine flip
        bool flip = parent.GetComponent<Level1>().getFlip();

        if (prevFlip && !flip){
            camera.localEulerAngles = initialCameraOrientation;
            StartCoroutine(RotateCoroutine(transform.localRotation, Quaternion.Euler(initialPivotOrientation)));
        }
        else if (!prevFlip && flip){
            camera.localEulerAngles = initialCameraOrientation;
            StartCoroutine(RotateCoroutine(transform.localRotation, Quaternion.Euler(new Vector3(315, 0, 0))));
        }

        turn.y += Input.GetAxis("Mouse Y");
        //transform.localRotation
        Quaternion temp = Quaternion.Euler(rotationSpeed * -turn.y, 0, 0);

        if (!flip) { //when we are right-side up
            if (100 < temp.eulerAngles.x && temp.eulerAngles.x <= 332 && !flip) {
                if (camera.eulerAngles.x > 333 && Input.GetAxis("Mouse Y") > 0)
                    camera.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0));    
                else if (camera.eulerAngles.x < 350 && Input.GetAxis("Mouse Y") < 0)
                    camera.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0));
                }
            else if (temp.eulerAngles.x <= 50 || temp.eulerAngles.x >= 200)
                transform.localRotation = temp;
            }
        else { //when we are upsidedown
            if (temp.eulerAngles.x < 330 && temp.eulerAngles.x > 285) {
                transform.localRotation = temp;
            }
        }
        prevFlip = flip;
        }

        IEnumerator RotateCoroutine(Quaternion startQuaternion, Quaternion endQuaternion) {
        float elapsedTime = 0f;
        
        while (elapsedTime < flipDuration)
        {
            float t = elapsedTime / flipDuration;

            transform.rotation = Quaternion.Slerp(startQuaternion, endQuaternion, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.localRotation= endQuaternion;
    }
}
