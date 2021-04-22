using UnityEngine;

namespace HOM
{
    public class SCustomerGetOrderState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var order = OrdersManager.CreateOrder(animator.gameObject).GetComponent<Order>();
            var vignette = animator.gameObject.GetComponentInChildren<OrderVignetteUI>();
            vignette.Activate();
            vignette.ResetIcons();
            vignette.UpdateInterface(order.Type, order.RedSouls, order.GreenSouls, order.BlueSouls, order.YellowSouls, order.PurpleSouls);

        }
    }
}
