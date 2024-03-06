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
    public void OnCollisionEnter(Collision collision){
        Debug.Log(collision.contacts[0].normal);
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
        if(_controller.isGrounded) {
            
            if(!flip) {
                if(Input.GetKeyDown("space")) {
                    ySpeed = jumpSpeed;
                }
                
            } 

        } else if (!flip) {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        } 
        if(flip) {
            //Need way to check for collision on top using temp solution
            if(true){

            
                ySpeed -= Physics.gravity.y * Time.deltaTime;
            } else {
                if(Input.GetKeyDown("space")) {
                    ySpeed = -jumpSpeed;
                }
            }
        }
        
        move.y = ySpeed;
        //Debug.Log(move);
        _controller.Move(move * Time.deltaTime * _speed);
        
        
    }

}
