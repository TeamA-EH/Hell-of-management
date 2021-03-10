using UnityEngine;

namespace HOM
{
    public class GarthBaseMovementState : StateMachineBehaviour
    {
        C_Garth controller;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!controller) controller = animator.gameObject.GetComponent<C_Garth>();
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!MovementHandler.AnyMovementKeyPressed())
            {
                animator.SetTrigger("Decelerate");
            }

            controller.Move();
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

    }
}