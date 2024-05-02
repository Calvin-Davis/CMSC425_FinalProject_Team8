using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBot : MonoBehaviour
{
    public Transform[] goals;
    private int idx;
    private NavMeshAgent agent;
    public bool isCyclic = true;
    private int inverse = 1;
    public float botSpeed = 5;
    
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
        //Walks to set node, once arives target changes to next node
        //cyclic decides whether to go back to first node at end or turn around at last node
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
    }
}
