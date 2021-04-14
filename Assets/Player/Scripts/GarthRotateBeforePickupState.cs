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
            if(C_Garth.self.handIndexReques == 0 && C_Garth.self.PlayerHands[1].m_canBind) C_Garth.self.AnimationController.SetFloat("Pickup ID", 0);
            else if(C_Garth.self.handIndexReques == 0 && !C_Garth.self.PlayerHands[1].m_canBind) C_Garth.self.AnimationController.SetFloat("Pickup ID", 2);
            else if(C_Garth.self.handIndexReques == 1 && C_Garth.self.PlayerHands[0].m_canBind) C_Garth.self.AnimationController.SetFloat("Pickup ID", 1);
            else if(C_Garth.self.handIndexReques == 1 && !C_Garth.self.PlayerHands[0].m_canBind) C_Garth.self.AnimationController.SetFloat("Pickup ID", 3);
            //Aggiungere condizione per il mocho che e' diversa da tutte le precedenti

            Debug.Log($"Pickup ID: {C_Garth.self.AnimationController.GetFloat("Pickup ID")}");

            C_Garth.self.AnimationController.SetTrigger("Start Pickup");
        }
    }
}

