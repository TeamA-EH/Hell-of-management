using System;
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
                    CreateSoulProjectile(ref projectile, SoulsManager.SOUL_TAG_RED);
                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(1));
                    SetSkillAnimation();

                    OnSkillCompleted?.Invoke();
                    return;
                case 1://Green soul
                    CreateSoulProjectile(ref projectile, SoulsManager.SOUL_TAG_GREEN);
                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(2));
                    SetSkillAnimation();

                    OnSkillCompleted?.Invoke();
                    return;
                case 2://Blue soul
                    CreateSoulProjectile(ref projectile, SoulsManager.SOUL_TAG_BLUE);
                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(3));
                    SetSkillAnimation();

                    OnSkillCompleted?.Invoke();
                    return;
                case 5: //Yellow soul
                    CreateSoulProjectile(ref projectile, SoulsManager.SOUL_TAG_YELLOW);
                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(10));
                    SetSkillAnimation();

                    OnSkillCompleted?.Invoke();
                break;
                case 6: //Purple soul
                    CreateSoulProjectile(ref projectile, SoulsManager.SOUL_TAG_PURPLE);
                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(11));
                    SetSkillAnimation();

                    OnSkillCompleted?.Invoke();
                break;    
                    case 3:
                    projectile = PlatesManager.CreatePlate(PlatesManager.DISH_PLATE, position, orientation);
                    projectile.GetComponent<Plate>().OverrideRecipeInfos(
                        C_Garth.self.PlayerHands[hand].plateInfos.Type,
                        null,
                        C_Garth.self.PlayerHands[hand].plateInfos.RedSouls,
                        C_Garth.self.PlayerHands[hand].plateInfos.GreenSouls,
                        C_Garth.self.PlayerHands[hand].plateInfos.BlueSouls);
                    projectile.GetComponent<Plate>().Init();
                    projectile.GetComponent<Plate>().EnablePhysics();
                    projectile.GetComponent<Plate>().SetForce((MovementHandler.ConvertMousePositionToWorldSpace() - C_Garth.self.gameObject.transform.position).normalized * maxSpeed);

                    SetSkillAnimation();


                    OnSkillCompleted?.Invoke();
                    return;
                    case 4:
                    projectile = PlatesManager.CreatePlate(PlatesManager.DRINK_PLATE, position, orientation);
                    projectile.GetComponent<Plate>().OverrideRecipeInfos(
                        C_Garth.self.PlayerHands[hand].plateInfos.Type,
                        null,
                        C_Garth.self.PlayerHands[hand].plateInfos.RedSouls,
                        C_Garth.self.PlayerHands[hand].plateInfos.GreenSouls,
                        C_Garth.self.PlayerHands[hand].plateInfos.BlueSouls);
                    projectile.GetComponent<Plate>().Init();
                    projectile.GetComponent<Plate>().EnablePhysics();
                    projectile.GetComponent<Plate>().SetForce((MovementHandler.ConvertMousePositionToWorldSpace() - C_Garth.self.gameObject.transform.position).normalized * maxSpeed);

                     SetSkillAnimation();


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

        ///<summary>Creates and initialize a soul like a projectile</summary>
        ///<param name="soul">The projectile reference</param>
        ///<param name="tag">The unique tag for this soul [check the SoulsManager.cs for tag constants]</param>
        void CreateSoulProjectile(ref GameObject soul, uint tag)
        {
            soul = SoulsManager.CreatesSoul(tag, position);
            soul.GetComponent<Soul>().Init();
            soul.GetComponent<Soul>().DeactivatesAgent();
            soul.GetComponent<Soul>().SetEnvironment(false);
            soul.GetComponent<Animator>().SetTrigger("Floating");
            soul.GetComponent<Soul>().ExecuteBehaviourTree();
            soul.GetComponent<Soul>().SetForce(((MovementHandler.ConvertMousePositionToWorldSpace() - C_Garth.self.gameObject.transform.position).normalized) * maxSpeed);
        }
        ///<summary>Sends the movement information for performing the correct animation for this skill</summary>
        void SetSkillAnimation()
        {
            if(C_Garth.self.PlayerHands[0].m_canBind && !C_Garth.self.PlayerHands[1].m_canBind) C_Garth.self.AnimationController.SetFloat("Item ID", 2);
            else if(!C_Garth.self.PlayerHands[0].m_canBind && C_Garth.self.PlayerHands[1].m_canBind) C_Garth.self.AnimationController.SetFloat("Item ID", 1);
            else if(C_Garth.self.PlayerHands[0].m_canBind && C_Garth.self.PlayerHands[1].m_canBind) C_Garth.self.AnimationController.SetFloat("Item ID", 0);
        }
    }
}
