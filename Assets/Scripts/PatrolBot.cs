using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBot : MonoBehaviour
{
    public Transform[] goals;
    private int idx, inverse = 1;
    private NavMeshAgent agent;
    private Renderer eyeRenderer;
    public bool isCyclic = true;
    private float animationTime = 0;
    public float botSpeed = 5, floatHeight = 0.4F, floatAnimationSpeed = 3F;
    public Material eyeColor;

    // Start is called before the first frame update
    void Start()
    {
        idx = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = botSpeed;
        agent.destination = goals[idx].position;

        // select eye color
        eyeRenderer = transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>();
        eyeRenderer.material = eyeColor;
    }

    // Update is called once per frame
    void Update()
    {
        //Walks to set node, once arives target changes to next node
        //cyclic decides whether to go back to first node at end or turn around at last node
        if (Mathf.Abs(transform.position.x - goals[idx].position.x) <= 0.1 && Mathf.Abs(transform.position.z - goals[idx].position.z) <= 0.1)
        {
            idx += 1 * inverse;

            if (idx == goals.Length)
            {
                if (isCyclic)
                {
                    idx = 0;
                }
                else
                {
                    idx -= 1;
                    inverse = -1;
                }

            }
            if (idx < 0)
            {
                idx = 1;
                inverse = 1;
            }
            agent.destination = goals[idx].position;
        }

        animateFloating();
    }

    //Similar to animateFloating() in ZoneBot class. Makes PatrolBot "float" up and down while moving
    void animateFloating()
    {
        float yPos = floatHeight * (Mathf.Cos(floatAnimationSpeed * animationTime + MathF.PI) + 1);
        transform.position = new Vector3(transform.position.x, transform.position.y + yPos, transform.position.z);

        if (animationTime >= 2 * MathF.PI / floatAnimationSpeed)
            animationTime = 0;

        animationTime += Time.deltaTime;
    }
}
