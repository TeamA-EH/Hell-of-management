using UnityEngine;

namespace HOM
{
   public class GarthDashState : StateMachineBehaviour
   {
       C_Garth character;
       DashSkill skill;

       float dashTime = 0.0f; // dash interpolation current time;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            MovementHandler.DisableCharacterRotation(animator.gameObject);

            if(!character) character = animator.gameObject.GetComponent<C_Garth>();
            if(skill == null) skill = SkillManager.GetSKill(SkillManager.SK_DASH) as DashSkill;

            dashTime = .0f;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(dashTime < skill.executionTime)
            {
                dashTime+= Time.deltaTime;
                character.Move(skill.characterSpeed.Evaluate(dashTime));
            }
            else
            {
                if(!MovementHandler.AnyMovementKeyPressed())
                {
                    animator.SetTrigger("Wait");
                }
                else
                {
                    animator.SetTrigger("Move");
                }
            }
            
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            MovementHandler.EnableCharacterRotation(animator.gameObject);
        }

 
 
   }
}
