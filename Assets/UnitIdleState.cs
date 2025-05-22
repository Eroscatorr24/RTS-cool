using UnityEngine;

public class UnitIdleState : StateMachineBehaviour
{
    AttackController attackController; // Variable para almacenar la referencia al AttackController de esta unidad

    // OnStateEnter se llama cuando comienza una transición y la máquina de estados comienza a evaluar este estado
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Obtenemos el componente AttackController del mismo GameObject que el Animator
        attackController = animator.GetComponent<AttackController>();
    }

    // OnStateUpdate se llama en cada fotograma de actualización entre las devoluciones de llamada OnStateEnter y OnStateExit
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Verificamos si el attackController de ESTA unidad tiene un targetToAttack asignado
        // Es buena práctica también verificar si attackController no es null antes de intentar acceder a sus miembros
        if (attackController != null && attackController.targetToAttack != null)
        {
            animator.SetBool("isFollow", true);
        }
    }

    // OnStateExit se llama cuando termina una transición y la máquina de estados termina de evaluar este estado
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // No se necesita nada aquí para este comportamiento específico, pero puedes añadir lógica si es necesario
    }
}
