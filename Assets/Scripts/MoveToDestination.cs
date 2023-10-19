using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveToDestination : MonoBehaviour
{
    //public variables/objects 
    NavMeshAgent agent;
    public Transform Destination;
    public float minDistance = 1;

    void Update()
    {
        //gets AI prefab and player destination every frame
        agent = GetComponent<NavMeshAgent>();
        agent.destination = Destination.position;

        //sets a distance between the player and the AI unit
        if (Vector3.Distance(agent.transform.position, Destination.position) < minDistance)
        {
            agent.destination = agent.transform.position;
        }
    }
}
