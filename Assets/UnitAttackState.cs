using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class UnitAttackState : StateMachineBehaviour
{
    NavMeshAgent agent;
    AttackController attackController;
    
    public float stopAttackingDistance= 1.2f;
    public float attackingDistance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        agent = animator.GetComponent<NavMeshAgent>();
        attackController = animator.GetComponent<AttackController>();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (attackController.targetToAttack != null &&
            animator.transform.GetComponent<UnitMovement>().isCommandedToMove == false)
        {
            
            LookAtTarget();
            agent.SetDestination(attackController.targetToAttack.position);
            
            float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.position, animator.transform.position);
            if (distanceFromTarget > attackingDistance || attackController.targetToAttack == null)
            {
                animator.SetBool("isAttack", false);
            }
        }
            
        }

    private void LookAtTarget()
    {
        Vector3 direction = attackController.targetToAttack.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);
        
        var yRotation = agent.transform.rotation.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        
    }
}


 

  