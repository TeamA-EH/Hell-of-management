using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateLoseSequence : StateMachineBehaviour
{
    private GameObject loseScreen;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        loseScreen = Resources.Load<GameObject>("TempLostScreen (Canvas)");
        Instantiate(loseScreen, new Vector3(0, 0, 0), Quaternion.identity);
        Time.timeScale = 0;
        Debug.Log("Lost");
    }
}
