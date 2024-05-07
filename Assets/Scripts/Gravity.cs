using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public CharacterController _controller;
    public GameObject parent;
    public bool startFlipped;
    private float ySpeed;
    private bool justTeleported = false;
    //used to flip gravity for cubes (other than player) in level

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pull level speed (can increase speed block falls at)
        float _speed = parent.GetComponent<Level1>().getSpeed();
        //pull if gravity is normal or flipped in level
        bool flip = parent.GetComponent<Level1>().getFlip();
        //this tag allows block to have opposite gravity of player if wanted for later levels
        if (startFlipped)
            flip = !flip;
        Vector3 move = new Vector3(0, 0, 0);
        //check if obj is with upside down gravity hit ceiling (equivalent of grounded
        //in flipped gravity)
        if (((_controller.collisionFlags & CollisionFlags.Above) != 0) & flip)
        {
            if (ySpeed > 0)
            {
                //in this cases remove its yvelocity so it does not try to go through
                //ceiling and will move immediately after gravity changes
                //note speed is set slightly above zero so the collision will continue to
                //be detected while the block stays on the ceiling since sometimes
                //collision was missed when speed was 0
                ySpeed = 0.2f;
            }
            //same case as above for normal grav
        }
        else if (_controller.isGrounded & !flip)
        {
            if (ySpeed < 0)
            {
                ySpeed = -0.2f;
            }
            //Used for edge case where someone flips gravity with high 
            //momentum right before hitting a roof/floor
        }
        else if (((_controller.collisionFlags & CollisionFlags.Above) != 0) & !flip & ySpeed > 0)
        {
            //if block hits the ceiling it should lose velocity much quicker than 
            //just to gravity
            ySpeed = -0.1f;
            //same as above for opposite gravity
        }
        else if (_controller.isGrounded & flip & ySpeed < 0)
        {
            ySpeed = 0.1f;
        }
        //giving acceleration for free falling cube
        else if (!flip)
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            ySpeed -= Physics.gravity.y * Time.deltaTime;
        }
        move.y = ySpeed;
        //commit the movement, unless a teleport has recently happened in the special case
        if (!justTeleported)
        {
            _controller.Move(move * Time.deltaTime * _speed);
        }
        
    }

    // This pair of methods is used for a special case to solve a race condition
    // where a cube would hit a teleporter as it's falling but the teleport wouldn't
    // execute due to the gravity script overruling it; this lets the Teleport script shut off
    // the gravity script for a fraction of a second to allow the teleport to execute fully
    // before continuing, but isn't used with all teleporters, just the ones
    // in the chambers in Level 5, though it can be applied to later levels if
    // necessary. 
    public void JustTeleported()
    {
        justTeleported = true;
        StartCoroutine(TeleportTimeout());
    }

    IEnumerator TeleportTimeout()
    {
        yield return new WaitForSeconds(0.2f);
        justTeleported = false;
    }
}
