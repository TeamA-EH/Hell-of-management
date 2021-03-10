using UnityEngine;

namespace HOM
{

    public class GarthBaseAccelerationState : StateMachineBehaviour
    {
        C_Garth controller;
        float accellerationTime;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!controller) controller = animator.gameObject.GetComponent<C_Garth>();
            accellerationTime = 0.0f;
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (MovementHandler.AnyMovementKeyPressed())
            {
                accellerationTime += Time.deltaTime;

                controller.AccelerateCharacter(accellerationTime, () =>
                 {
                     Debug.Log("Acceleration Completed");
                     animator.SetTrigger("Move");
                 });
            }
            else
            {
                animator.SetTrigger("Decelerate");
            }
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}