using UnityEngine;

namespace HOM
{
    public class SoulAwaitState : StateMachineBehaviour
    {
        Soul target = null;
        C_Garth player;
        float distance = 0.0f; // current distance from player
        float maxDistance = 0.0f;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!target) target = animator.gameObject.GetComponent<Soul>();
            if(!player) player = C_Garth.self;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(target.BehaviourTreeActivated)
            {
                Debug.Log("RUn");
                if(target.IsFloating())
                {
                    animator.SetTrigger("Floating");
                    return;
                }

                distance = (C_Garth.self.gameObject.transform.position - animator.gameObject.transform.position).magnitude;
                if(distance < target.MaxDistanceFromPlayer && target.InsideRoom)
                {
                    animator.SetTrigger("Escape From Player");
                }
                else if(!target.InsideRoom)
                {
                    animator.SetTrigger("Escape From Hell");
                }
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}
