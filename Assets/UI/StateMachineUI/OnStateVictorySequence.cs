using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateVictorySequence : StateMachineBehaviour
{
    private GameObject victoryScreen;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        victoryScreen = Resources.Load<GameObject>("TempWonScreen (Canvas)");
        Instantiate(victoryScreen, new Vector3(0, 0, 0), Quaternion.identity);
        Time.timeScale = 0;
        Debug.Log("Won");
    }
}
