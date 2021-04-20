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
                    InitializeSoulProjectile(ref projectile, SoulsManager.SOUL_TAG_RED);                 
                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(1));
                    SetSkillAnimation();

                    OnSkillCompleted?.Invoke();
                return;
                case 1: //Green soul
                    InitializeSoulProjectile(ref projectile, SoulsManager.SOUL_TAG_GREEN);
                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(2));
                    SetSkillAnimation();

                    OnSkillCompleted?.Invoke();
                return;
                case 2: //Blue soul
                    InitializeSoulProjectile(ref projectile, SoulsManager.SOUL_TAG_BLUE);
                    MovementHandler.DecreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(3));
                    SetSkillAnimation();

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
                    projectile.GetComponent<Plate>().SetForce((MovementHandler.ConvertMousePositionToWorldSpace() - C_Garth.self.gameObject.transform.position).normalized * maxSpeed);

                    SetSkillAnimation();


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
                    projectile.GetComponent<Plate>().SetForce((MovementHandler.ConvertMousePositionToWorldSpace() - C_Garth.self.gameObject.transform.position).normalized * maxSpeed);

                   SetSkillAnimation();


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
        ///<summary>Creates and initialize a soul like a projectile</summary>
        ///<param name="data">The projectile reference</param>
        ///<param name="tag">The unique tag for this soul [check the SoulsManager.cs for tag constants]</param>
        void InitializeSoulProjectile(ref GameObject data, uint tag)
        {
            data = SoulsManager.CreatesSoul(tag, position);
            SoulsManager.SetSoulTag(data.GetComponent<Soul>(), tag);
            data.GetComponent<Soul>().Init();
            data.GetComponent<Soul>().DeactivatesAgent();
            data.GetComponent<Soul>().EnablePhysics();
            data.GetComponent<Soul>().SetEnvironment(false);
            data.GetComponent<Animator>().SetTrigger("Floating");
            data.GetComponent<Soul>().ExecuteBehaviourTree(); //Avvia l'intelligenza artificiale delle anime all'entry-point(AWAIT STATE)
            data.GetComponent<Soul>().SetForce(((MovementHandler.ConvertMousePositionToWorldSpace() - C_Garth.self.gameObject.transform.position).normalized + Vector3.up) * maxSpeed);
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