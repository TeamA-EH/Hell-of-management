using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class GarthDropState : StateMachineBehaviour
    {
        DropSkill skill = null;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(skill == null) skill = SkillManager.GetSKill(SkillManager.SK_DROP) as DropSkill;
            skill.Execute(() =>
            {
                animator.ResetTrigger("Drop");
                animator.SetTrigger("Wait");
            });
        }
    }
}
