using UnityEngine;

namespace HOM
{
    public sealed class SCustomerLeaveTable : StateMachineBehaviour
    {
        SittedCustomer target = null;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!target) target = animator.gameObject.GetComponent<SittedCustomer>();

            GameObject customer = null;

            animator.gameObject.GetComponentInParent<Chair>().RegistredPlate.SetActive(false);
            animator.gameObject.GetComponentInParent<Chair>().UnregisterPlate();

            /* SPAWN MOVABLE CUSTOMER */
            if(target.gameObject.name == "Female_Customer_Sitted")
            {
                customer = CustomersManager.self.SpawnFemaleCustomer("Go To Exit", false);
            }
            else if(target.gameObject.name == "Male_Customer_Sitted")
            {
                customer = CustomersManager.self.SpawnMaleCustomer("Go To Exit", false);
            }

            /* DEFINES CUSTOMER SPAWN POINT */
            if(animator.gameObject.transform.rotation == Quaternion.Euler(0,0,0))
            {
                customer.transform.position = animator.gameObject.transform.position - animator.gameObject.transform.forward;
            }
            else if(animator.gameObject.transform.rotation == Quaternion.Euler(0,90,0))
            {
                customer.transform.position = animator.gameObject.transform.position - animator.gameObject.transform.right;
            }
            else if(animator.gameObject.transform.rotation == Quaternion.Euler(0,-90,0))
            {
                customer.transform.position = animator.gameObject.transform.position + animator.gameObject.transform.right;
            }
            else if(animator.gameObject.transform.rotation == Quaternion.Euler(0,180,0))
            {
                customer.transform.position = animator.gameObject.transform.position + animator.gameObject.transform.forward;
            }

            animator.gameObject.GetComponentInParent<Chair>().Unlock();
            target.gameObject.SetActive(false);
        }
    }
}
