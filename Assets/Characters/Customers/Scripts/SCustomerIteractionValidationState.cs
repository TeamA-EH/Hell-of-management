using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class SCustomerIteractionValidationState : StateMachineBehaviour
    {
        SittedCustomer customer;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           if(!customer) customer = animator.gameObject.GetComponent<SittedCustomer>();

           if(customer.CanIteract)
           {
               animator.SetTrigger("Iteraction Enabled");
           }
           else
           {
               animator.SetTrigger("Iteraction Disabled");
           }
        }
    }
}
