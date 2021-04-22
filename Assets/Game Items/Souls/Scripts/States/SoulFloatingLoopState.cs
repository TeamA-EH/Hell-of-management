using UnityEngine;
using UnityEngine.Animations;

namespace HOM
{
    public class SoulFloatingLoopState : StateMachineBehaviour
    {
        Soul target = null;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            if(!target) target = animator.gameObject.GetComponent<Soul>();
            target.SetAIState(Soul.MachineState.FLOATING);
            target.DeactivatesAgent();
            Debug.Log("Deativated");
            //target.DeactivatesArtificialIntelligence();
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!target.IsFloating())
            {
                animator.SetTrigger("End Floating");
            }
        }
    }
}
