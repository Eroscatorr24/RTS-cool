using UnityEngine;
using UnityEngine.AI;
// using UnityEngine.Animations; // UnityEngine.Animations no es necesario para este script

public class UnitFollowState : StateMachineBehaviour
{
    AttackController attackController;
    NavMeshAgent agent;
    public float attackingDistance = 1f;

    // OnStateEnter se llama cuando comienza una transición y la máquina de estados comienza a evaluar este estado
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        attackController = animator.GetComponent<AttackController>();
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate se llama en cada fotograma de actualización entre las devoluciones de llamada OnStateEnter y OnStateExit
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        if (attackController == null || attackController.targetToAttack == null)
        {
            animator.SetBool("isFollow", false);
            return; // Salimos temprano si no hay objetivo
        }
        else
        {
            if(animator.transform.GetComponent<UnitMovement>().isCommandedToMove == false)
            {
            agent.SetDestination(attackController.targetToAttack.position);
            animator.transform.LookAt(attackController.targetToAttack);
            
            float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.position, animator.transform.position);
            if (distanceFromTarget < attackingDistance)
            {
                agent.SetDestination(animator.transform.position);
               animator.SetBool("isAttack", true);
            }
            }
            
        }

       
    }

    private NavMeshAgent SetDestination(Vector3 transformPosition)
    {
        throw new System.NotImplementedException();
    }
}
