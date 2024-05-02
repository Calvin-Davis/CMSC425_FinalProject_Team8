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
        on = startOn;
        onPos = Quaternion.Euler(0, 90, -90 - turnRadius);
        offPos = Quaternion.Euler(0, 90, -90 + turnRadius);

        handle.localRotation = startOn ? onPos : offPos;

        receiver = receiverObject.GetComponent<ILeverInteractable>();
        if (receiver == null)
        {
            Debug.LogWarning("The receiver does not implement ILeverInteractable!", this);
        }

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
        if (!schmactive && (CC.isGrounded || ((CC.collisionFlags & CollisionFlags.Above) != 0)))
        {
            schmactive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

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
