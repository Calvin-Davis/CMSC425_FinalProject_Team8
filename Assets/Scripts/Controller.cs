using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject parent;

    public GameObject pivot;
    

    [SerializeField] public float jumpSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        _controller = GetComponent<CharacterController>();
    }
    public void OnTriggerEnter(Collider other){
        if(other.name == "Teleport") {
            Debug.Log("Teleporting");
            
            //StartCoroutine(Teleport(0, 1, 0));
        }
    }
    // public void OnCollisionEnter(Collision collision){
    //     Vector3 normal = collision.contacts[0].normal;
    //     if(normal == cube.up) {

    //     } else if(normal == cube.down) {

    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        float _speed = parent.GetComponent<Level1>().getSpeed();
        bool flip = parent.GetComponent<Level1>().getFlip();
        //float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        //float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        // yRotation += mouseX;
        // xRotation += mouseY;
        // xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        // orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        Vector3 move = new Vector3(0,0,0);
        if(!flip) {
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        } else {
            move = new Vector3(-Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        if(Input.GetKeyDown("q")){
            //flip = !flip;
            pivot.transform.Rotate(0.0f,0.0f,180.0f);
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
        } else if (((_controller.collisionFlags & CollisionFlags.Above) != 0) & !flip & ySpeed > 0) {
            //Used for edge case where someone flips gravity with high 
            //momentum right before hitting a roof/floor
            //Note at this point in this code when something 
            //collides where corners just barely touch leading to
            // May fix itself by changing character model
            ySpeed = -0.1f;
        } else if(_controller.isGrounded & flip & ySpeed < 0) {
            ySpeed = 0.1f;
        }
        else if (!flip) {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        } else {
            ySpeed -= Physics.gravity.y * Time.deltaTime;
        } 
        move.y = ySpeed;
        Physics.SyncTransforms();
        _controller.Move(move * Time.deltaTime * _speed);
        
        
    }
    IEnumerator Teleport(int x, int y, int z) {
        transform.position = new Vector3(x, y, z);
        
        yield return null;
    
    }
}
