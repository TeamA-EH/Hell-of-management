using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public sealed class SoulStopMovementState :  StateMachineBehaviour
    {
        Soul target = null;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!target) target = animator.gameObject.GetComponent<Soul>();
            target.DeactivatesArtificialIntelligence();
            animator.SetTrigger("Idle");
        }
    }
}
