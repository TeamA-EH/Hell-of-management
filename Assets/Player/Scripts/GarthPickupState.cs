using UnityEngine;
namespace HOM
{
    public class GarthPickupState : StateMachineBehaviour
    {
        PickupSkill skill = null;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(skill == null) skill = SkillManager.GetSKill(SkillManager.SK_PICKUP) as PickupSkill;
            skill.Execute(() =>
            {
                MovementHandler.EnableCharacterRotation(animator.gameObject);
                C_Garth.self.AnimationController.SetTrigger("End Pickup");
                animator.SetTrigger("Wait");
            });
        }

        
    }
}