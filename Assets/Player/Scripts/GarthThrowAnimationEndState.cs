using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class GarthThrowAnimationEndState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            MovementHandler.EnableCharacterRotation(C_Garth.self.gameObject);
            MovementHandler.EnableInput(C_Garth.self);
        }
       
    }
}
