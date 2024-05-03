using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.AI;

public class ComplexZoneBot : MonoBehaviour
{
    public Transform[] goals;
    private int idx, inverse = 1;
    private float animationTime = 0;
    private bool pursuiting = false, prevPursuiting = false;
    private Renderer eyeRenderer;
    private NavMeshAgent agent;
    public bool isCyclic = true;
    public bool resting = true;
    public GameObject player;
    public float botSpeed = 5, radius = 1, floatHeight = 0.4F, floatAnimationSpeed = 5F;
    public Transform defaultGoal;
    public Material neutralEyeColor, pursuitEyeColor;
    public AudioSource alertSound;
    
    // Start is called before the first frame update
    void Start()
    {
        idx = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = botSpeed;
        agent.destination = goals[idx].position;

        eyeRenderer = transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>();
        setEyeColor(neutralEyeColor);
    }

    // Update is called once per frame
    void Update()
    {
        //This bot acts like combination of other bots (is purple in game).
        //Like patrol bot it goes to given nodes one by one, but if a player gets close
        //enough to one of the nodes it the bot will target the player instead until the
        //player leaves range upon which the bot will continue where it left off
        //If resting is set to true the bot will behave more like a zone bot sitting
        //at the designated node until a player gets within specified range of one of
        //the nodes (note this allows for creation of a zone bot with a non-circular zone)

        //check if player in range of nodes
        bool inRange = false;
        foreach (Transform t in goals) {
            float distance = (player.transform.position - t.position).magnitude;
            if(distance < radius) {
                inRange = true;
                pursuiting = true;
            } else {
                inRange = false;
                pursuiting = false;
            }
        }

        if (pursuiting && !prevPursuiting) {
            setEyeColor(pursuitEyeColor);
            alertSound.PlayDelayed(0);
        }

        else if (!pursuiting && prevPursuiting) {
            setEyeColor(neutralEyeColor);
        }

        //follow set node path or if player in range then tarher player
        //(patrol bot code + player targeting)
        if(!resting) {
            if(!inRange) {
                agent.destination = goals[idx].position;
                if(Mathf.Abs(transform.position.x - goals[idx].position.x) <= 0.1 && Mathf.Abs(transform.position.z - goals[idx].position.z) <= 0.1) {
                    idx += 1*inverse;
                    
                    if (idx == goals.Length) {
                        if(isCyclic) {
                            idx = 0;
                        } else {
                            idx -= 1; 
                            inverse = -1;
                        }
                        
                    }
                    if(idx < 0) {
                        idx = 1;
                        inverse = 1;
                    }
                    agent.destination = goals[idx].position;
                }
            } else {
                agent.destination = player.transform.position;
            }
        } else {
            //go to default position until player in range (zone bot code)
             if(inRange) {
                agent.destination = player.transform.position;
             } else {
                agent.destination = defaultGoal.position;
             }
        }

        animateFloating();

        prevPursuiting = pursuiting;
    }

    void setEyeColor(Material color) {
        eyeRenderer.material = color;
    }

    void animateFloating()
    {
        float yPos = floatHeight * (Mathf.Cos(floatAnimationSpeed * animationTime + MathF.PI) + 1);
        transform.position = new Vector3(transform.position.x, transform.position.y + yPos, transform.position.z);

        if (animationTime >= 2 * MathF.PI / floatAnimationSpeed)
            animationTime = 0;

        animationTime += Time.deltaTime;
    }
}
