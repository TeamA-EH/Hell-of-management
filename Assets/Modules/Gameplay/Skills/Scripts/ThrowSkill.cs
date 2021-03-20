using System;
using System.Collections;
using UnityEngine;

namespace HOM
{
    public class ThrowSkill : ISKill
    {
        float maxSpeed = 0.0f;

        uint itemType = 0;
        uint hand = 0;
        Vector3 position = Vector3.zero;
        Quaternion orientation = Quaternion.identity;

        public ThrowSkill(SkillData settings)
        {
            maxSpeed = settings.MaxSpeed;
        }

        public void Execute(Action OnSkillCompleted = null)
        {
            C_Garth.self.PlayerHands[hand].UnbindBindedItem();
            GameObject projectile = null;
            switch(itemType)
            {
                case 0: //Red soul

                    projectile = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_RED, position);
                    projectile.GetComponent<Soul>().SetForce(C_Garth.self.gameObject.transform.forward * maxSpeed);


                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(1));

                    OnSkillCompleted?.Invoke();
                    return;
                case 1://Green soul

                    projectile = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_GREEN, position);
                    projectile.GetComponent<Soul>().SetForce(C_Garth.self.gameObject.transform.forward * maxSpeed);

                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(2));

                    OnSkillCompleted?.Invoke();
                    return;
                case 2://Blue soul

                    projectile = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_BLUE, position);
                    projectile.GetComponent<Soul>().SetForce(C_Garth.self.gameObject.transform.forward * maxSpeed);

                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(3));

                    OnSkillCompleted?.Invoke();
                    return;
            }
        }

        public void Stop(Action OnSkillStopped = null)
        {
            
        }

        ///<summary> Overrides throw general infos before activates skill </summary>
        ///<param name="itemType"> The holded item uinque id: 0=red soul, 1=green soul, 2=blue soul </param>
        ///<param name="hand"> The hand holding the item unique index </param>
        ///<param name="position"> The hand position </param>
        ///<param name="orientation"> The hand orientation </param>
        public void OverrideSkillInfo(uint itemType, uint hand, Vector3 position, Quaternion orientation)
        {
            this.itemType = itemType;
            this.hand = hand;
            this.position = position;
            this.orientation = orientation;
        }
    }
}
