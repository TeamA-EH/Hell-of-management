using UnityEngine;
using DG.Tweening;
public class PlayerTurn : StateMachineBehaviour
{
    MCController controller;
    Quaternion rotation;
    float angle;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!controller) controller = animator.gameObject.GetComponentInParent<MCController>();

        rotation = animator.gameObject.transform.rotation;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.rotation = rotation * Quaternion.Euler(0, controller.CameraAngularDirection, 0);
    }


}