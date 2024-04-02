using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public CharacterController _controller;
    public GameObject parent;
    private float ySpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _speed = parent.GetComponent<Level1>().getSpeed();
        bool flip = parent.GetComponent<Level1>().getFlip();
        Vector3 move = new Vector3(0, 0, 0);
        if (((_controller.collisionFlags & CollisionFlags.Above) != 0) & flip)
        {
            if(ySpeed > 0) {
                ySpeed = 0.2f;
            }
        } else if(_controller.isGrounded & !flip) {
            if(ySpeed < 0) {
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
}
