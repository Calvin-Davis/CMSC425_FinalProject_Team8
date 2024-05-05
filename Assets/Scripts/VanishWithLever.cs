using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishWithLever : MonoBehaviour, ILeverInteractable
{
    public Material enabled;
    public Material disabled;
    public bool startOn = false;

    private BoxCollider collider;
    private UnityEngine.AI.NavMeshObstacle agent;

    // Start is called before the first frame update
    void Start()
    {
        //enables renders, updates navmesh and puts collider on if default state is on
        collider = GetComponent<BoxCollider>();
        collider.enabled = startOn;

        agent = GetComponent<UnityEngine.AI.NavMeshObstacle>();
        if (agent != null)
        {
            agent.enabled = startOn;
        }

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
        //enables or disables based on lever state

        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            collider.enabled = newState;
            if (agent != null)
            {
                agent.enabled = newState;
            }

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
