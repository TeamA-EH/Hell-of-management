using System;
using System.Collections;
using UnityEngine;

namespace HOM
{
    public class ThrowSkill : ISKill
    {
        float maxSpeed = 0.0f;
        AnimationCurve movementCurve = null;

        uint itemType = 0;
        uint hand = 0;
        Vector3 position = Vector3.zero;
        Quaternion orientation = Quaternion.identity;
        float skillExecutionTime = 0.0f;

        public ThrowSkill(SkillData settings)
        {
            maxSpeed = settings.MaxSpeed;
        }

        public void Execute(Action OnSkillCompleted = null)
        {
            C_Garth.self.PlayerHands[hand].UnbindBindedItem();
            GameObject projectile = null;
            skillExecutionTime = 0.0f;
            switch(itemType)
            {
                case 0: //Red soul

                    projectile = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_RED, position);
                    while (skillExecutionTime < movementCurve.keys[1].time)
                    {
                        skillExecutionTime+=Time.deltaTime;
                        projectile.GetComponent<Soul>().SetForce(projectile.transform.forward * maxSpeed);
                    }

                break;
                case 1://Green soul

                    projectile = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_GREEN, position);
                    while (skillExecutionTime < movementCurve.keys[1].time)
                    {
                        skillExecutionTime+=Time.deltaTime;
                        projectile.GetComponent<Soul>().SetForce(projectile.transform.forward * maxSpeed);;
                    }

                break;
                case 2://Blue soul

                    projectile = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_BLUE, position);
                    while (skillExecutionTime < movementCurve.keys[1].time)
                    {
                        skillExecutionTime+=Time.deltaTime;
                        projectile.GetComponent<Soul>().SetForce(projectile.transform.forward * maxSpeed);
                    }

                break;
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
