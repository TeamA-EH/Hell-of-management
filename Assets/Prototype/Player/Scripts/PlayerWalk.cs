using UnityEngine;

public class PlayerWalk : StateMachineBehaviour
{
    MCController controller;
    Vector3 inputDirection => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!controller) controller = animator.gameObject.GetComponentInParent<MCController>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetKeyDown(controller.dashKey))
        {
            animator.SetTrigger("Dash");
            return;
        }

        animator.SetFloat("Camera Angle", controller.CameraAngularDirection);
        animator.SetFloat("speed", controller.PlayerSpeed);

        if (controller.CameraAngularDirection == 0 || controller.CameraAngularDirection == 90 || controller.CameraAngularDirection == -90 || controller.CameraAngularDirection == 180)
        {
            animator.SetFloat("Player Angle", 0);
        }
        else animator.SetFloat("Player Angle", controller.CameraAngularDirection);


    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}