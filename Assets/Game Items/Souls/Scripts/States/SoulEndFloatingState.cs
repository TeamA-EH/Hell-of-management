using UnityEngine;

namespace HOM
{
    public class SoulEndFloatingState : StateMachineBehaviour
    {
        Soul target = null;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!target) target = animator.gameObject.GetComponent<Soul>();
            target.SetAIState(Soul.MachineState.GROUNDED);

            if(target.InsideRoom) animator.SetTrigger("Escape From Player");
            else if(!target.InsideRoom) animator.SetTrigger("Escape From Hell");
            else animator.SetTrigger("Idle");
        }
    }
}