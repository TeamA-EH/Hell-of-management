﻿using UnityEngine;

namespace HOM
{
    public sealed class SCustomerDespawnState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           animator.gameObject.SetActive(false);
        }
    }
}