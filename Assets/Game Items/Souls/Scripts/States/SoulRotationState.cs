using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class SoulRotationState : StateMachineBehaviour
    {
        Soul target;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!target) target = animator.gameObject.GetComponent<Soul>();

            target.AIRotate(.2f, () => {

                if(target.GetAISpeed() == 0)
                {
                    animator.SetTrigger("Accellerate Soul");
                }
                else
                {
                    animator.SetTrigger("Move Soul");
                }


            });
        }
    }
}
