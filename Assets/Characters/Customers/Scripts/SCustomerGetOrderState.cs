using UnityEngine;

namespace HOM
{
    public class SCustomerGetOrderState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var order = OrdersManager.CreateOrder(animator.gameObject);
        }
    }
}
