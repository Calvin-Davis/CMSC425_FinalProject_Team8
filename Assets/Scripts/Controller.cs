using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Controller : MonoBehaviour
{
    private CharacterController _controller;
    private float ySpeed;
    public GameObject parent;

    public Transform camera;

    public AudioSource audioSource;
    private bool grounded = true;
    public float rotationSpeed = 2;
    Vector2 turn;
    Vector3 forwardDirection, sideDirection;

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
            
        }
    }
    void groundedCheck()
    {
        float distance;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            distance = hit.distance;
            if(Physics.Raycast(transform.position,Vector3.down,distance + 0.1f))
            {
                grounded = true;
            } else {
                grounded = false;
            }

        } 
    } 

    void MovementAudio() 
    {
        if(Input.GetAxis("Horizontal") != 0  || Input.GetAxis("Vertical") != 0){
            if(grounded){
                audioSource.enabled = true;
            }
        } else {
            audioSource.enabled = false;
        }
    }

    void rotateController() {
        Vector3 camdir = camera.forward;
        forwardDirection = camdir;
        transform.rotation = Quaternion.Euler(0f, camera.eulerAngles.y, 0f);
        sideDirection = Vector3.Cross(forwardDirection, Vector3.up);
    }

    void Update()
    {
        rotateController();
        groundedCheck();
        float _speed = parent.GetComponent<Level1>().getSpeed();
        bool flip = parent.GetComponent<Level1>().getFlip();
        
        Vector3 forwardMove = Vector3.Scale(new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Vertical")), forwardDirection);
        Vector3 sideMove = Vector3.Scale(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Horizontal")), sideDirection);

        Vector3 move = forwardMove - sideMove;
        
        if (((_controller.collisionFlags & CollisionFlags.Above) != 0) & flip)
        {
            if(Input.GetKeyDown("space")) {
                ySpeed -= jumpSpeed;
            } else if(ySpeed > 0) {
                ySpeed = 0.5f;
            }
        } else if(_controller.isGrounded & !flip) {
            if(Input.GetKeyDown("space")) {
                ySpeed += jumpSpeed;
            } else if(ySpeed < 0) {
                ySpeed = -0.5f;
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
        MovementAudio();
    }
    IEnumerator Teleport(int x, int y, int z) {
        transform.position = new Vector3(x, y, z);
        
        yield return null;
    
    }
}
