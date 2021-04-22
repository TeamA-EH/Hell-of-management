using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class SoulDestroyState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.gameObject.SetActive(false);
        }
    }
}
