using UnityEngine;
using DG.Tweening;


namespace HOM
{
    public sealed class SoulMovementState : StateMachineBehaviour
    {
        Soul target = null;
        float distanceFromPlayer = 0.0f;
        float stuckTime = 0.0f;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           
            if(!target) target = animator.gameObject.GetComponent<Soul>();
            target.ActivatesArtificialIntelligence();

        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            distanceFromPlayer = (C_Garth.self.gameObject.transform.position - animator.gameObject.transform.position).magnitude;
            if(target.AIReachedGoal())
            {
                if(distanceFromPlayer <= target.MaxDistanceFromPlayer)
                {
                    animator.SetTrigger("Calculate Pattern");
                }
                else
                {
                    animator.SetTrigger("Stop Soul");
                }
            }

            stuckTime += Time.deltaTime;
            if(stuckTime >= 5)
            {
                animator.SetTrigger("Calculate Pattern");
            }

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

       
    }
}
