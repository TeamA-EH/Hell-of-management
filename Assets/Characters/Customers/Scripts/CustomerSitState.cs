using UnityEngine;

namespace HOM
{
    public class CustomerSitState : StateMachineBehaviour
    {
        Customer controller;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!controller) controller = animator.gameObject.GetComponent<Customer>();
            controller.chair.EnableCustomerGFX();
            animator.SetTrigger("Idle");
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            controller.gameObject.SetActive(false);
        }
    }
}
