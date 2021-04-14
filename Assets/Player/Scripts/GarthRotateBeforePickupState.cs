using UnityEngine;
using DG.Tweening;

namespace HOM
{
    public class GarthRotateBeforePickupState : StateMachineBehaviour
    {
    
        GameObject target = null;
        PickupSkill skill = null;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(skill == null) skill = SkillManager.GetSKill(SkillManager.SK_PICKUP) as PickupSkill;
            target = skill.ObjToDrag;

            /* Calculate Angle*/
            Vector3 direction = (target.transform.position - animator.gameObject.transform.position).normalized;
            float dot = Vector3.Dot(animator.gameObject.transform.forward, direction);
            float length = animator.gameObject.transform.forward.magnitude * direction.magnitude;
            float angle = Mathf.Acos(dot * length) * Mathf.Rad2Deg;

            animator.gameObject.transform.DORotate(new Vector3(0, angle, 0), .2f);
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            C_Garth.self.AnimationController.SetTrigger("Start Pickup");
        }
    }
}

