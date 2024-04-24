using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishWithLever : MonoBehaviour, ILeverInteractable
{
    public Material enabled;
    public Material disabled;
    public bool startOn = false;

    private BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        collider.enabled = startOn;

        Renderer rend = GetComponent<Renderer>();
        rend.material = startOn ? enabled : disabled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeverToggle(bool newState)
    {
        // newstate: describes whether the lever is now on (true) or off (false)

        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            collider.enabled = newState;

            if (newState)
            {
                rend.material = enabled;
            } 
            else
            {
                rend.material = disabled;
            }
        }

        //this.gameObject.SetActive(newState);
    }
}
