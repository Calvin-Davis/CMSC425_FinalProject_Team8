using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.AI;

public class ComplexZoneBot : MonoBehaviour
{
    public Transform[] goals;
    private int idx;
    private NavMeshAgent agent;
    public bool isCyclic = true;
    private int inverse = 1;
    public float botSpeed = 5;
    public bool resting = true;
    public GameObject player;
    public float radius = 1;
    public Transform defaultGoal;
    
    // Start is called before the first frame update
    void Start()
    {
        idx = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = botSpeed;
        agent.destination = goals[idx].position;
    }

    // Update is called once per frame
    void Update()
    {
        bool inRange = false;
        foreach (Transform t in goals) {
            float distance = (player.transform.position - t.position).magnitude;
            if(distance < radius) {
                inRange = true;
            } else {
                inRange = false;
            }
        }


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
             if(inRange) {
                agent.destination = player.transform.position;
             } else {
                agent.destination = defaultGoal.position;
             }
        }
    }
}
