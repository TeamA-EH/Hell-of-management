using UnityEngine;

namespace HOM
{
    public class GarthRotationState : StateMachineBehaviour
    {
        C_Garth controller = null;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!controller) controller = animator.gameObject.GetComponent<C_Garth>();
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            controller.RotateCharacter(true, null);
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}