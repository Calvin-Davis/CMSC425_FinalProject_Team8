using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private CharacterController _controller;
    private float ySpeed, rotationTime = 0;
    private bool walking;
    private Transform leftLeg, rightLeg;
    public GameObject parent;

    public Transform camera;

    public AudioSource audioSource;
    private bool grounded = true;
    public float rotationSpeed = 2, walkAnimationSpeed = 10;
    Vector3 forwardDirection, sideDirection;

    [SerializeField] public float jumpSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        //used to keep cursor in game and invisibile so it does not block view of game
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        _controller = GetComponent<CharacterController>();
        leftLeg = transform.GetChild(3).transform.GetChild(0).transform.GetChild(4);
        rightLeg = transform.GetChild(3).transform.GetChild(0).transform.GetChild(5);
    }

    //used to keep player model looking in same direction as camera
    void rotateController() {
        Vector3 camdir = camera.forward;
        forwardDirection = camdir;
        transform.rotation = Quaternion.Euler(0f, camera.eulerAngles.y, 0f);
        sideDirection = Vector3.Cross(forwardDirection, Vector3.up);
    }

    void Update()
    {
        rotateController();

        float _speed = parent.GetComponent<Level1>().getSpeed();
        bool flip = parent.GetComponent<Level1>().getFlip();
        
        Vector3 forwardMove = Vector3.Scale(new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Vertical")), forwardDirection);
        Vector3 sideMove = Vector3.Scale(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Horizontal")), sideDirection);

        Vector3 move = forwardMove - sideMove;
        
        //look at gravity.cs for more in depth explanation. this is similar code
        //that also adds jump functionality when the player is determined to be grounded
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

        if ((move.x != 0 || move.z != 0) && (_controller.isGrounded || (_controller.collisionFlags & CollisionFlags.Above) != 0))
            walking = true;
        else
            walking = false;

        animateWalking();        
    }

    private void animateWalking() {
        // If we are walking then rotate legs according to sine wave and play walking audio.
        if (walking) {
            audioSource.enabled = true;
            rotationTime += Time.deltaTime;
            leftLeg.localEulerAngles = new Vector3(Mathf.Sin(rotationTime * walkAnimationSpeed) * 45, Mathf.Sin(rotationTime * walkAnimationSpeed) * 20, 0);
            rightLeg.localEulerAngles = new Vector3(Mathf.Sin(rotationTime * walkAnimationSpeed) * -45, Mathf.Sin(rotationTime * walkAnimationSpeed) * -20, 0);
            if (rotationTime >= (2 * Mathf.PI / rotationTime))
                rotationTime = 0;
        }
        // Otherwise return legs to default orientation and turn off walking audio
        else {
            audioSource.enabled = false;
            leftLeg.localEulerAngles = new Vector3(0, 0, 0);
            rightLeg.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    
}

