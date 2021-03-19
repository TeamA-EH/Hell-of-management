using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class GarthThrowState : StateMachineBehaviour
    {
        ThrowSkill skill = null;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            skill = SkillManager.GetSKill(SkillManager.SK_THROW) as ThrowSkill;
            skill.Execute();
        }
    }
}
