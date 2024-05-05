using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Lever : MonoBehaviour
{
    public GameObject receiverObject;
    public Transform handle;
    public float turnRadius;
    public bool startOn = false;

    public Transform wireParent;
    public Material wireOffMaterial;
    public Material wireOnMaterial;

    public AudioSource leverSound;


    private bool on;
    private Quaternion onPos;
    private Quaternion offPos;
    private ILeverInteractable receiver;
    private List<Renderer> wireRenderers;
    bool schmactive = true;

    public CharacterController CC;

    // Start is called before the first frame update
    void Start()
    {
        //set lever to strating pos (can be on or off depeending on startOn)
        on = startOn;
        onPos = Quaternion.Euler(0, 90, -90 - turnRadius);
        offPos = Quaternion.Euler(0, 90, -90 + turnRadius);

        handle.localRotation = startOn ? onPos : offPos;

        //pulls object that lever is supposed to impact
        receiver = receiverObject.GetComponent<ILeverInteractable>();
        if (receiver == null)
        {
            Debug.LogWarning("The receiver does not implement ILeverInteractable!", this);
        }

        //puts object the lever modifies in base state (which state this is depends on
        //starOn)
        if (wireParent != null)
        {
            wireRenderers = new List<Renderer>();
            foreach (Transform child in wireParent)
            {
                Renderer rend = child.GetComponent<Renderer>();
                if (rend != null)
                {
                    wireRenderers.Add(rend);
                    rend.material = startOn ? wireOnMaterial : wireOffMaterial;
                }
                Collider coll = child.GetComponent<Collider>();
                if (coll != null)
                {
                    coll.enabled = false;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //variable used to keep player from hitting the lever twice in one jump
        //QOL as in testing it was found to be very annoying to be able to hit
        //the lever once on the way up and down on accident
        if (!schmactive && (CC.isGrounded || ((CC.collisionFlags & CollisionFlags.Above) != 0)))
        {
            schmactive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //flips reciever to opposite state when lever and changes lever position
        //(i.e. up or down) when lever is hit
        if (schmactive) {
            schmactive = false;
            leverSound.Play();
            on = !on;
            Material newMaterial = null;
            if (on)
            {
                handle.localRotation = onPos;
                newMaterial = wireOnMaterial;
            } else
            {
                handle.localRotation = offPos;
                newMaterial = wireOffMaterial;
            }

            if (wireRenderers != null)
            {
                foreach (Renderer rend in wireRenderers)
                {
                    rend.material = newMaterial;
                }
            }

            receiver.LeverToggle(on);

        }
    }
}
