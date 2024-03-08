using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Controller : MonoBehaviour
{
    private CharacterController _controller;
    // public float sensX;
    // public float sensY;
    // public Transform orientation;
    // float xRotation;
    // float yRotation;
    private float ySpeed;
    private bool flip;

    [SerializeField] public float jumpSpeed = 10;
    [SerializeField] private float _speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        //float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        // yRotation += mouseX;
        // xRotation += mouseY;
        // xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        // orientation.rotation = Quaternion.Euler(0, yRotation, 0);   
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(Input.GetKeyDown("q")){
            flip = !flip;
        }
        if (((_controller.collisionFlags & CollisionFlags.Above) != 0) & flip)
        {
            if(Input.GetKeyDown("space")) {
                ySpeed -= jumpSpeed;
            } else if(ySpeed > 0) {
                ySpeed = 0.2f;
            }
        } else if(_controller.isGrounded & !flip) {
            if(Input.GetKeyDown("space")) {
                ySpeed += jumpSpeed;
            } else if(ySpeed < 0) {
                ySpeed = -0.2f;
            }
        } else if (!flip) {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        } else {
            ySpeed -= Physics.gravity.y * Time.deltaTime;
        } 
        move.y = ySpeed;
        Debug.Log(ySpeed);
        _controller.Move(move * Time.deltaTime * _speed);
        
    }

}
