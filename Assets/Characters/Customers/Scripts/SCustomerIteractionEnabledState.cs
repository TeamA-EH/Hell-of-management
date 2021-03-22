using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class SCustomerIteractionEnabledState : StateMachineBehaviour
    {
        SittedCustomer customer = null;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!customer) customer = animator.GetComponent<SittedCustomer>();
            
            /* AGGIUNGERE QUANDO VERRANNO FORNITE LE MESH DEI PERSONAGGI CON I MATERIALI */
            //animator.gameObject.GetComponentInChildren<MeshRenderer>().material = customer.CustomerIteractionData.IteractableShader;
        }
    }
}
