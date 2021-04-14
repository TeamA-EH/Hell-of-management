using UnityEngine;

namespace HOM
{
    public class GarthWaitState : StateMachineBehaviour
    {
        DashSkill dash = null;
        bool firstEntry = true;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(dash == null) dash = SkillManager.GetSKill(SkillManager.SK_DASH) as DashSkill;
            if(!firstEntry) C_Garth.self.AnimationController.SetTrigger("Idle");
            firstEntry = false;
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(MovementHandler.AnyMovementKeyPressed())
            {
                animator.SetTrigger("Accelerate");
            }

            if(Input.GetKeyDown(KeyCode.Space) && dash.CanActivate)
            {
                animator.SetTrigger("Dash");
            }
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}