using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class GarthPickupAnimationStartState : StateMachineBehaviour
    {
        public override void  OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           C_Garth.self.AnimationController.SetFloat("speed", 0);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            C_Garth.self.gameObject.GetComponent<Animator>().SetTrigger("Pickup Item");
        }
    }
}