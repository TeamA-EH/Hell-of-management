using System;
using UnityEngine;
using DG.Tweening;

namespace HOM
{
    public class PickupSkill : MonoBehaviour, ISKill
    {

        internal GameObject ObjToDrag;
        uint objType;
        uint handID;

        public void Execute(Action OnSkillCompleted = null)
        {
            var hand = C_Garth.self.PlayerHands[handID];
            switch(objType)
            {
                case 0: 
                    hand.BindSoul(SoulsManager.SOUL_TAG_RED);
                break;
                case 1: 
                    hand.BindSoul(SoulsManager.SOUL_TAG_GREEN);
                break;
                case 2: 
                    hand.BindSoul(SoulsManager.SOUL_TAG_BLUE);
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
        ///<param name="type"> The draggable item unique type: 0 = Red Soul, 1 = Green Soul, 2 = Blue Soul, 3 = Waste, 4 = Trashbag, 5 = Female Demon, 6 = Male Demon </param>
        ///<param name="hand"> The character hand unique index </param>
        public void OverridePickupInfos(GameObject draggable, uint type, uint hand)
        {
            ObjToDrag = draggable;
            objType = type;
            handID = hand;
        }

    }
}
