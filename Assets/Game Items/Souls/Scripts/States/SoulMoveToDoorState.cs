using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class SoulMoveToDoorState : StateMachineBehaviour
    {
        public Vector3 door;
        Soul target;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!target) target = animator.gameObject.GetComponent<Soul>();
            target.SetAIGoal(door);
            target.ActivatesArtificialIntelligence();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(target.AIReachedGoal())
            {
                animator.SetTrigger("Destroy Soul");
            }
        }
    }
}
