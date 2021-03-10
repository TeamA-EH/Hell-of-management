using System;
using System.Collections;
using UnityEngine;

namespace HOM
{
    public class DashSkill : ISKill
    {
        DashData ddata;
        public float executionTime => ddata.m_executionTime;
        public float cooldownTime => ddata.m_cooldownTime;
        public AnimationCurve characterSpeed => ddata.m_speedTweening;
        bool canDash = true;
        
        ///<summary> Return TRUE if is not disabled </summary>
        public bool CanActivate => canDash;

        public DashSkill(DashData settings)
        {
            ddata = settings;
        }

        public void Execute(Action OnSkillCompleted = null)
        {
            C_Garth.self.StartCoroutine(ExecuteDashLifecycle(C_Garth.self.gameObject, OnSkillCompleted));
        }

        public void Stop(Action OnSkillStopped = null)
        {
            var anim = C_Garth.self.gameObject.GetComponent<Animator>();

            if(!MovementHandler.AnyMovementKeyPressed())
            {
                anim.SetTrigger("Wait");
            }
            else
            {
                anim.SetTrigger("Move");
            }

            OnSkillStopped?.Invoke();
        }

        ///<summary> Manages activation and deactivation states for this skill </summary>
        ///<param name="character"> The dashing character </param>
        ///<param name="OnCompleted"> Callbacks Called when the dash ends </param>
        IEnumerator ExecuteDashLifecycle(GameObject character, Action OnCompleted = null)
        {
            var animator = character.GetComponent<Animator>();
            animator.SetTrigger("Dash");
            yield return new WaitForSeconds(executionTime);
            canDash = false;

            OnCompleted?.Invoke();
            yield return new WaitForSeconds(cooldownTime);
            canDash = true;
        }
    }
}
