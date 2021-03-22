using UnityEngine;

namespace HOM
{

    public class GarthBaseAccelerationState : StateMachineBehaviour
    {
        C_Garth controller;
        float accellerationTime;

        DashSkill dash = null;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!controller) controller = animator.gameObject.GetComponent<C_Garth>();
            if(dash == null) dash = SkillManager.GetSKill(SkillManager.SK_DASH) as DashSkill;
            accellerationTime = 0.0f;
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (MovementHandler.AnyMovementKeyPressed())
            {
                accellerationTime += Time.deltaTime;

                controller.AccelerateCharacter(accellerationTime, () =>
                {
                    animator.SetTrigger("Move");
                });

                if(Input.GetKeyDown(KeyCode.Space) && dash.CanActivate)
                {
                    dash.Execute();
                    return;
                }

                
            }
            else
            {
                animator.SetTrigger("Decelerate");
            }
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}