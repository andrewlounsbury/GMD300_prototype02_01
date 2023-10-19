using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentPatrol : MonoBehaviour
{
    //patrol script we made in class that I am not using for this project 
    public Transform[] PatrolPoints;
    private NavMeshAgent agent;
    private int currentIndex = 0; 

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(PatrolPoints[0].position);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentIndex = (currentIndex + 1) % PatrolPoints.Length;
        }
    }
}
