using UnityEngine;

namespace HOM
{
    public class GarthBaseDecelerationState : StateMachineBehaviour
    {
        C_Garth controller;
        float decelerationTime = 0.0f;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!controller) controller = animator.gameObject.GetComponent<C_Garth>();
            decelerationTime = 0.0F;
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!MovementHandler.AnyMovementKeyPressed())
            {
                decelerationTime += Time.deltaTime;

                controller.DecelerateCharacter(decelerationTime, () =>
                {
                    animator.SetTrigger("Wait");
                });
            }
            else
            {
                animator.SetTrigger("Accelerate");
            }
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}