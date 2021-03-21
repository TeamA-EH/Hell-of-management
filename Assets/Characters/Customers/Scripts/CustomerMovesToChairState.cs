using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class CustomerMovesToChairState : StateMachineBehaviour
    {
        Customer controller = null;
        Vector3 goal = Vector3.zero;
        float distanceToGoal => (goal - controller.gameObject.transform.position).magnitude;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!controller) controller = animator.gameObject.GetComponent<Customer>();
            goal = controller.chair.gameObject.transform.position;
            controller.Agent.destination = goal;
            controller.Agent.isStopped = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(distanceToGoal < 1)
            {
                controller.Agent.isStopped = true;
                animator.SetTrigger("Sit");
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           
        }
    }

}
