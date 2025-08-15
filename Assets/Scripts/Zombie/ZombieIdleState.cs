using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class ZombieIdleState : StateMachineBehaviour
{
    float timer;
    public float idleTime = 0f;

    Transform player;

    public float detectionAreaRadius = 18f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       timer = 0;
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //Transition to Patrol State

       timer += Time.deltaTime;
       if (timer > idleTime)
       {
        animator.SetBool("isPatroling", true);
       }

       //Transition to Chase state

       float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
       if (distanceFromPlayer < detectionAreaRadius)
       {
            animator.SetBool("isChasing", true);
       }
    }

    
}
