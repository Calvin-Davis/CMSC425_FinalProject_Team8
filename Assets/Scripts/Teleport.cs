using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] public float x = 0;
    [SerializeField] public float y = 1;
    [SerializeField] public float z = 0;

    // By default, the destination is specified using absolute coordinates. 
    // However, this allows for a GameObject's transform's position to be 
    // used as a destination instead of having to figure out the absolute coordinates.
    public bool useTransform = false;

    public Transform destination;

    public AudioSource teleSound;

    // This field serves to fix a bug that resulted in some cubes not teleporting; in
    // level 5 each cube is essentially mapped to one teleporter, so this will be used
    // to communicate the teleportation to the cube's gravity script.
    public Gravity gravity = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //if anything collides with the teleporter it will be tped to set destination
        //set destination can be given as a 3d coordinate or an object to tp to
        teleSound.PlayDelayed(0);

        if (!useTransform)
        {
            if (gravity != null)
            {
                gravity.JustTeleported();
            }
            other.transform.position = new Vector3(x, y, z);
        }
        else
        {
            if (gravity != null)
            {
                gravity.JustTeleported();
            }
            other.transform.position = destination.position;
        }
    }

}
