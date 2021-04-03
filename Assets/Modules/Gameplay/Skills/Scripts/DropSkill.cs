using System;
using UnityEngine;

namespace HOM
{
    public class DropSkill : ISKill
    {
        float maxSpeed = 0.0f;
        uint type = 0;
        uint hand = 0;
        Vector3 position = Vector3.zero;
        Quaternion orientation = Quaternion.identity;

        public DropSkill(SkillData settings)
        {
            maxSpeed=settings.MaxSpeed;
        }

        public void Execute(Action OnSkillCompleted = null)
        {
            C_Garth.self.PlayerHands[hand].UnbindBindedItem();
            GameObject projectile = null;
            switch(type)
            {
                case 0: //Red soul
                    projectile = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_RED, position);
                    SoulsManager.SetSoulTag(projectile.GetComponent<Soul>(), SoulsManager.SOUL_TAG_RED);
                    projectile.GetComponent<Soul>().Init();
                    projectile.GetComponent<Soul>().EnablePhysics();
                    projectile.GetComponent<Soul>().SetEnvironment(false);
                    projectile.GetComponent<Soul>().ExecuteBehaviourTree();
                    projectile.GetComponent<Soul>().SetForce(C_Garth.self.gameObject.transform.forward * maxSpeed);
                    
                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(1));

                    OnSkillCompleted?.Invoke();
                return;
                case 1: //Green soul
                    projectile = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_GREEN, position);
                    SoulsManager.SetSoulTag(projectile.GetComponent<Soul>(), SoulsManager.SOUL_TAG_GREEN);
                    projectile.GetComponent<Soul>().Init();
                    projectile.GetComponent<Soul>().EnablePhysics();
                    projectile.GetComponent<Soul>().SetEnvironment(false);
                    projectile.GetComponent<Soul>().ExecuteBehaviourTree();
                    projectile.GetComponent<Soul>().SetForce(C_Garth.self.gameObject.transform.forward * maxSpeed);

                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(2));

                    OnSkillCompleted?.Invoke();
                return;
                case 2: //Blue soul
                    projectile = SoulsManager.CreatesSoul(SoulsManager.SOUL_TAG_BLUE, position);
                    SoulsManager.SetSoulTag(projectile.GetComponent<Soul>(), SoulsManager.SOUL_TAG_BLUE);
                    projectile.GetComponent<Soul>().Init();
                    projectile.GetComponent<Soul>().EnablePhysics();
                    projectile.GetComponent<Soul>().SetEnvironment(false);
                    projectile.GetComponent<Soul>().ExecuteBehaviourTree();
                    projectile.GetComponent<Soul>().SetForce(C_Garth.self.gameObject.transform.forward * maxSpeed);

                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(3));

                    OnSkillCompleted?.Invoke();
                return;
                case 3: //Dish Plate
                    projectile = PlatesManager.CreatePlate(PlatesManager.DISH_PLATE, position, orientation);
                    projectile.GetComponent<Plate>().OverrideRecipeInfos(
                        C_Garth.self.PlayerHands[hand].plateInfos.Type,
                        null,
                        C_Garth.self.PlayerHands[hand].plateInfos.RedSouls,
                        C_Garth.self.PlayerHands[hand].plateInfos.GreenSouls,
                        C_Garth.self.PlayerHands[hand].plateInfos.BlueSouls);
                    projectile.GetComponent<Plate>().Init();
                    projectile.GetComponent<Plate>().EnablePhysics();
                    projectile.GetComponent<Plate>().SetForce(C_Garth.self.gameObject.transform.forward * maxSpeed);

                    OnSkillCompleted?.Invoke();
                break;
                case 4: //Drink Plate
                    projectile = PlatesManager.CreatePlate(PlatesManager.DRINK_PLATE, position, orientation);
                    projectile.GetComponent<Plate>().OverrideRecipeInfos(
                        C_Garth.self.PlayerHands[hand].plateInfos.Type,
                        null,
                        C_Garth.self.PlayerHands[hand].plateInfos.RedSouls,
                        C_Garth.self.PlayerHands[hand].plateInfos.GreenSouls,
                        C_Garth.self.PlayerHands[hand].plateInfos.BlueSouls);
                    projectile.GetComponent<Plate>().Init();
                    projectile.GetComponent<Plate>().EnablePhysics();
                    projectile.GetComponent<Plate>().SetForce(C_Garth.self.gameObject.transform.forward * maxSpeed);

                    OnSkillCompleted?.Invoke();
                break;
            }
        }

        public void Stop(Action OnSkillStopped = null)
        {

        }

        //<summary> Overrides drop general infos before activates skill </summary>
        ///<param name="type"> The holded item uinque id: 0=red soul, 1=green soul, 2=blue soul 3=dish plate, 4=drink plate </param>
        ///<param name="hand"> The hand holding the item unique index </param>
        ///<param name="position"> The hand position </param>
        ///<param name="orientation"> The hand orientation </param>
        public void OverrideSkillInfo(uint type,uint hand, Vector3 position, Quaternion orientation)
        {
            this.type = type;
            this.hand = hand;
            this.position = position;
            this.orientation = orientation;
        }
    }
}