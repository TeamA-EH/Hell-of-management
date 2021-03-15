using System;
using UnityEngine;

namespace HOM
{
    public class PickupSkill : MonoBehaviour, ISKill
    {
        public void Execute(Action OnSkillCompleted = null)
        {
            C_Garth.self.gameObject.GetComponent<Animator>().SetTrigger("Pickup");
        }
        public void Stop(Action OnSkillStopped = null)
        {

        }

    }
}
