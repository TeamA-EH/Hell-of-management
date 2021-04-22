using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace HOM
{
    public class SCustomerWaitPlayerIteractionState : StateMachineBehaviour
    {
        SittedCustomer customer = null;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!customer) customer = animator.gameObject.GetComponent<SittedCustomer>();
            customer.OnCustomerClicked += OnMouseClick;
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            customer.OnCustomerClicked -= OnMouseClick;
        }

        void OnMouseClick()
        {
            if(OrdersManager.self.activeOrders < OrdersManager.maxRecipesCount)
            {
                customer.gameObject.GetComponent<Animator>().SetTrigger("Create Order");
            }
        }
    }
}
