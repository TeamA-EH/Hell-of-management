using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public sealed class SCustomerMoveToDoor : StateMachineBehaviour
    {
        Customer target = null;
        Vector3 goal = Vector3.zero;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!target) target = animator.gameObject.GetComponent<Customer>();
            goal = CustomersManager.self.PubDoor.transform.position;
            target.Agent.destination = goal;
            target.Agent.isStopped = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float distance = (CustomersManager.self.PubDoor.transform.position - animator.gameObject.transform.position).magnitude;
            if(distance <= 1)
            {
                target.Agent.isStopped = true;
                animator.SetTrigger("Despawn");
                return;
            }
        }
    }
}
