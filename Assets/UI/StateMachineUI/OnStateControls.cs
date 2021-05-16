using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class OnStateControls : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GUIHandler.ActivatesMenu("Controls Menu");
            if (LevelManager.self.currentIndex == 0)
                GUIHandler.DeactivatesMenu("Main Menu");
            else
                GUIHandler.DeactivatesMenu("Pause Menu");
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (LevelManager.self.currentIndex == 0)
                GUIHandler.ActivatesMenu("Main Menu");
            else
                GUIHandler.ActivatesMenu("Pause Menu");
            GUIHandler.DeactivatesMenu("Controls Menu");
        }
    }
}
