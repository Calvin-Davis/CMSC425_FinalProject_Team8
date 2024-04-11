using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 2;
    Vector2 turn;
    
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        turn.y += Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(rotationSpeed * -turn.y, 0, 0);
    }
}
