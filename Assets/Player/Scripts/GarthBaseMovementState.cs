using UnityEngine;

namespace HOM
{
    public class GarthBaseMovementState : StateMachineBehaviour
    {
        C_Garth controller;
        DashSkill dash;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!controller) controller = animator.gameObject.GetComponent<C_Garth>();
            if(dash == null) dash = SkillManager.GetSKill(SkillManager.SK_DASH) as DashSkill;
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!MovementHandler.AnyMovementKeyPressed())
            {
                animator.SetTrigger("Decelerate");
            }

            if(Input.GetKeyDown(KeyCode.Space) && dash.CanActivate)
            {
                dash.Execute();
                return;
            }

            controller.Move();
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

    }
}