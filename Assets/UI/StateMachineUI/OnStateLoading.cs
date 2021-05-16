using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateLoading : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
    }
}
