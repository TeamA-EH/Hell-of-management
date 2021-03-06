﻿using UnityEngine;
using UnityEngine.Animations;

namespace HOM
{
    public class GarthThrowAnimationStartState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            C_Garth.self.AnimationController.SetFloat("speed", 0);
            MovementHandler.DisableCharacterRotation(C_Garth.self.gameObject);
            MovementHandler.DisableInput(C_Garth.self);
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            C_Garth.self.gameObject.GetComponent<Animator>().SetTrigger("Throw Item");
        }
    }
}
