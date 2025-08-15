using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;


public class ZombiePatrolingState : StateMachineBehaviour
{
    float timer;
    public float patrolingTime = 10f;

    Transform player;
    UnityEngine.AI.NavMeshAgent agent;

    public float detectionArea = 18f;
    public float patrolSpeed = 2f;

    List<Transform> waypointsList = new List<Transform>();
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //Initialization
       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();

       agent.speed = patrolSpeed;
       timer = 0;

       //Move to First Waypoint

       GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
       foreach (Transform t in waypointCluster.transform)
       {
            waypointsList.Add(t);
       }

       Vector3 nextPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
       agent.SetDestination(nextPosition);
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //Check if agent arrived at waypoint

       if (agent.remainingDistance <= agent.stoppingDistance)
       {
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
       }
       
        //Transition to Idle State

        timer += Time.deltaTime;
        if (timer > patrolingTime)
        {
            animator.SetBool("isPatroling", false);
        }

        //Transition to Chase state

       float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
       if (distanceFromPlayer < detectionArea)
       {
            animator.SetBool("isChasing", true);
       }
    }
    

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //Stop Agent
       agent.SetDestination(agent.transform.position);
    }
}
