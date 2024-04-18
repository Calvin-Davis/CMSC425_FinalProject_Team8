using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZoneBot : MonoBehaviour
{
    public float botSpeed = 5;
    public Vector3 Center = Vector3.zero;
    public float radius = 1;
    private NavMeshAgent agent;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed= botSpeed;
        agent.destination = Center;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (player.transform.position - Center).magnitude;
        if (distance < radius) {
            agent.destination = player.transform.position;
        } else {
            agent.destination = Center;
        }
    }
}
