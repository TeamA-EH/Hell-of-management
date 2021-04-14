using System;
using UnityEngine;
using DG.Tweening;

namespace HOM
{
    public class PickupSkill : ISKill
    {

        internal GameObject ObjToDrag;
        uint objType;
        public uint handID {private set; get;}

        public void Execute(Action OnSkillCompleted = null)
        {
            var hand = C_Garth.self.PlayerHands[handID];
            switch(objType)
            {
                case 0: 
                    hand.BindSoul(SoulsManager.SOUL_TAG_RED);
                    MovementHandler.IncreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(1));

                    /* SETUP ANIMATION ID*/
                    if(handID == 0 && C_Garth.self.PlayerHands[1].m_canBind)
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 1);
                    }
                    else if(handID == 1 && C_Garth.self.PlayerHands[0].m_canBind)
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 2);
                    }
                    else
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 3);
                    }
                break;
                case 1: 
                    hand.BindSoul(SoulsManager.SOUL_TAG_GREEN);
                    MovementHandler.IncreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(2));
                    
                    /* SETUP ANIMATION ID*/
                    if(handID == 0 && C_Garth.self.PlayerHands[1].m_canBind)
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 1);
                    }
                    else if(handID == 1 && C_Garth.self.PlayerHands[0].m_canBind)
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 2);
                    }
                    else
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 3);
                    }
                break;
                case 2: 
                    hand.BindSoul(SoulsManager.SOUL_TAG_BLUE);
                    MovementHandler.IncreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(3));
                    
                     /* SETUP ANIMATION ID*/
                    if(handID == 0 && C_Garth.self.PlayerHands[1].m_canBind)
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 1);
                    }
                    else if(handID == 1 && C_Garth.self.PlayerHands[0].m_canBind)
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 2);
                    }
                    else
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 3);
                    }
                break;
                case 3:

                break;
                case 4:

                break;
                case 5:

                break;
                case 6:

                break;
                case 7: // DISH PLATE
                    hand.BindPlate(1);
                    MovementHandler.IncreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(8));

                     /* SETUP ANIMATION ID*/
                    if(handID == 0 && C_Garth.self.PlayerHands[1].m_canBind)
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 1);
                    }
                    else if(handID == 1 && C_Garth.self.PlayerHands[0].m_canBind)
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 2);
                    }
                    else
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 3);
                    }
                break;
                case 8: // DRINK PLATE
                    hand.BindPlate(2);
                    MovementHandler.IncreaseItemWeight(C_Garth.self, MovementHandler.GetWeight(9));
                    
                    /* SETUP ANIMATION ID*/
                    if(handID == 0 && C_Garth.self.PlayerHands[1].m_canBind)
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 1);
                    }
                    else if(handID == 1 && C_Garth.self.PlayerHands[0].m_canBind)
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 2);
                    }
                    else
                    {
                        C_Garth.self.AnimationController.SetFloat("Item ID", 3);
                    }
                break;
            }

            ObjToDrag.SetActive(false);
            OnSkillCompleted?.Invoke();
        
        }
        public void Stop(Action OnSkillStopped = null)
        {
            Debug.LogWarning("Attention! This mehthod has not yet been implemented for this game version");
        }

        ///<summary> Changes values for pickup movement </summary>
        ///<param name="draggable"> Item to drag </param>
        ///<param name="type"> The draggable item unique type: 0 = Red Soul, 1 = Green Soul, 2 = Blue Soul, 3 = Waste, 4 = Trashbag, 5 = Female Demon, 6 = Male Demon, 7 = Dish Plate, 8= Drink Plate </param>
        ///<param name="hand"> The character hand unique index </param>
        public void OverridePickupInfos(GameObject draggable, uint type, uint hand)
        {
            ObjToDrag = draggable;
            objType = type;
            handID = hand;
        }

    }
}
