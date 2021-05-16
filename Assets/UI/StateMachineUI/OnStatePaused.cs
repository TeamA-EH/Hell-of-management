using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class OnStatePaused : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GUIHandler.ActivatesMenu("Pause Menu");
            GUIHandler.DeactivatesMenu("Clock");
            GUIHandler.DeactivatesMenu("ToDoList");
            Time.timeScale = 0;
        }
    }
}
