using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class OnStateResume : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GUIHandler.DeactivatesMenu("Pause Menu");
            GUIHandler.ActivatesMenu("Clock");
            GUIHandler.ActivatesMenu("ToDoList");
            Time.timeScale = 1;
        }
    }
}
