using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class ZombieChaseState : StateMachineBehaviour
{

        NavMeshAgent agent;
        Transform player;

        public float chaseSpeed = 6f;
        public float stopChasingDistance = 21;
        public float attackingDistance = 2.5f; 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Initialization
       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent = animator.GetComponent<NavMeshAgent>();

       agent.speed = chaseSpeed;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(player.position);
       animator.transform.LookAt(player);

       float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

       //Checking if agent should stop chasing

       if (distanceFromPlayer > stopChasingDistance)
       {
            animator.SetBool("isChasing", false);
       }

       //Checking if agent should Attack

       if (distanceFromPlayer < attackingDistance)
       {
            animator.SetBool("isAttacking", true);
       }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(animator.transform.position);
    }
}
