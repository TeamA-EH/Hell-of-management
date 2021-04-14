using UnityEngine;

namespace HOM
{
    public class GarthThrowState : StateMachineBehaviour
    {
        ThrowSkill skill = null;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            skill = SkillManager.GetSKill(SkillManager.SK_THROW) as ThrowSkill;
            skill.Execute(() =>{
                C_Garth.self.AnimationController.ResetTrigger("Start Throw");
                C_Garth.self.AnimationController.SetTrigger("End Throw");
                animator.ResetTrigger("Throw");
                animator.SetTrigger("Wait");
            });
        }
    }
}
