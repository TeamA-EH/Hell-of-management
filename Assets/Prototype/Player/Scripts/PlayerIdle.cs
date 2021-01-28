using UnityEngine;
using DG.Tweening;

public sealed class PlayerIdle : StateMachineBehaviour
{
    Vector3 playerDirection = Vector3.zero;
    MCController controller;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerDirection = animator.gameObject.transform.forward;

        if (!controller) controller = animator.gameObject.GetComponentInParent<MCController>();


    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*
         * Disattivato ai fini del testing per i designer - riattivare in seguito
         */
        //if (controller.PlayerAngularDirection != 0) animator.SetTrigger("Turn");
        //else if (controller.PlayerSpeed > 0.1f) animator.SetTrigger("Walk");

        if (controller.PlayerSpeed > 0.1f)
        {

            animator.gameObject.transform.DORotateQuaternion(Quaternion.Euler(0, controller.CameraAngularDirection, 0) * Quaternion.Euler(0, 90, 0), 0.2f)
                .OnComplete(() =>
                {
                    animator.SetTrigger("Walk");
                    animator.gameObject.transform.DOKill();
                });
        }


    }

   
}