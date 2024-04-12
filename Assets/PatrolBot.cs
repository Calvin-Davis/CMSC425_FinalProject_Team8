using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBot : MonoBehaviour
{
    public Transform[] goals;
    private int idx;
    private NavMeshAgent agent;
    
    
    // Start is called before the first frame update
    void Start()
    {
        idx = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goals[idx].position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Mathf.Abs(transform.position.x - goals[idx].position.x) <= 0.1 && Mathf.Abs(transform.position.z - goals[idx].position.z) <= 0.1) {
            idx += 1;
            
            if (idx >= goals.Length) {
                idx = 0;
            }
            agent.destination = goals[idx].position;
            Debug.Log(goals[idx].position);
        }
    }
}
