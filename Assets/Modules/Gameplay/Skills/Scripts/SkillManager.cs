using System;
using UnityEngine;

namespace HOM
{
    public class SkillManager : MonoBehaviour
    {
        static SkillManager self;       //Singleton reference

        [Header("Settings"), Space(10)]
        [SerializeField] DashData dashData;
        [SerializeField] SkillData throwData;
        [SerializeField] SkillData dropData;

        /* SKILLS UNIQUES IDENTIFIERS 
        *  These identifiers allows SKILL-CALL from user functions.
        */
        public const uint SK_NULL = 0x000000;
        public const uint SK_DASH = 0x000001;
        public const uint SK_PICKUP = 0x000002;
        public const uint SK_DROP = 0x000003;
        public const uint SK_THROW = 0x000004;
        public const uint skill_size = 4;
        
        enum ErrorCode { 
            ERROR_NO_IMPLEMENTATION,
            ERROR_SKILL_CALLED,
            ERROR_SURREAL_SKILL
            }  

        ISKill[] skills;

        #region Unity Callbacks
        void Start()
        {
            Init();
        }
        #endregion

        ///<summary> Intializes skill manager resources </summary>
        void Init()
        {
            if(!self) self = this;          //Setups singleton reference

            /* Validates initialization fields */
            if(!dashData)
            {
                Debug.LogError("Attention! None persistent data linked for [dashData] field!");
                return;
            }

            /* SETUP SKILL TREE */
            skills  = new ISKill[4];
            skills[0] = new DashSkill(dashData);
            skills[1] = new PickupSkill();
            Debug.LogWarning("Add Drop Skill");
            skills[3] = new ThrowSkill(throwData);
        }

        ///<summary> Activates one skill from the character skills </summary>
        ///<param name="skill"> Skill to activates uniques identifier </param>
        ///<param name="OnSkillCalled"> Callback called when the skill has been activated </param>
        public static void ActivatesSkill (uint skill, Action OnSkillCalled = null)
        {
            if(!(skill > skill_size))
            {
                self.skills[skill].Execute(OnSkillCalled);
            }
            else    // Error Handling
            {
                Debug.LogError($"Attention! None skill with identifier [{skill}] exists!");
            }

        }

        ///<summary> Stops the skill execution immediately </summary>
        ///<param name="skill"> Skill to stop </param>
        ///<param name="OnSkillStopped"> Callback called when the skill has been stopped </param>
        public static void StopSkill(ref ISKill skill, Action OnSkillStopped = null)
        {
            skill.Stop(OnSkillStopped);
        }

        public static ISKill GetSKill(uint skill) => self.skills[skill-1];

        ///<summary> Requests a pickup skill activation for a game object </summary>
        ///<param name="obj"> The game object to pickup </param>
        ///<param name="type"> The obj unique identfier: 0 = Red Soul, 1 = Green Soul, 2 = Blue Soul, 3 = Waste, 4 = Trashbag, 5 = Female Demon, 6 = Male Demon </param>
        ///<param name="character"> The character who have to hold this item </param>
        ///<param name="hand"> The hand unique index </param>
        ///<param name="OnSuccess"> Callback called when the request has been processed with success </param>
        ///<param name="OnFailure"> Callback called when the request hasn't been processed with success </param>
        public static void SendPickupSkillRequest(GameObject obj, uint type, GameObject character, uint hand, Action OnSuccess = null, Action OnFailure = null)
        {
            var hands = character.GetComponent<C_Garth>().PlayerHands;

            if(hands[hand].m_canBind)
            {
                var skill = GetSKill(SK_PICKUP) as PickupSkill;
                skill.OverridePickupInfos(obj, type, hand);

                character.GetComponent<Animator>().SetTrigger("Pickup");
                OnSuccess?.Invoke();
            }
            else
            {
                //Debug.LogAssertion("Attention! Pickup request failed!");
                OnFailure?.Invoke();
            }
            
        }

        ///<summary> Request a drop or throw skill activation based on the item usability mod </summary>
        ///<param name="character"> The player character referece </param>
        ///<param name="hand"> The hand unique index </param>
        ///<param name="OnSuccess"> Callback called when the request has been processed with success </param>
        ///<param name="OnFailure">  Callback called when the request hasn't been proecess with failure </param>
        public static void SendObjectReleaseRequest(GameObject character, uint hand, Action OnSuccess = null, Action OnFailure = null)
        {
            var hands = character.GetComponent<C_Garth>().PlayerHands;

            if(!hands[hand].m_canBind)
            {

                switch(SkillUsageManager.self.ItemMod)
                {
                    case SkillUsageManager.SelectedType.THROW:

                        var skill = GetSKill(SK_THROW) as ThrowSkill;
                        skill.OverrideSkillInfo(hands[hand].holdedItemIndex, 0, hands[hand].gameObject.transform.position, Quaternion.identity);
                        character.gameObject.GetComponent<Animator>().SetTrigger("Throw");
                        OnSuccess?.Invoke();

                    break;
                    case SkillUsageManager.SelectedType.DROP:

                        //get skill
                        //override skill info
                        character.gameObject.GetComponent<Animator>().SetTrigger("Drop");
                        OnSuccess?.Invoke();

                    break;
                }
            }
            else
            {
                Debug.Log("Attention! The character hand is empty!");
                OnFailure?.Invoke();
            }
        }

    }
}