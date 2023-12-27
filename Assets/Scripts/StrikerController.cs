using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StrikerController : MonoBehaviour
{
    public Transform[] waypoints;   
    private int currentWaypointIndex = 0;  
    private NavMeshAgent navMeshAgent;

    public bool Run = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = Random.Range(10f, 14f); 

        SetDestination();
    }

    void Update()
    {
       
            if (navMeshAgent.remainingDistance < 0.1f && !navMeshAgent.pathPending)
            {
                SetNextWaypoint();
            }
        
    }

    void SetDestination()
    {
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    void SetNextWaypoint()
    {
        if (Run)
        {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

        SetDestination();

            
        }
    }

    void StartRun()
    {

    }
}
