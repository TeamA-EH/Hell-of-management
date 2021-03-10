using System;
using UnityEngine;

namespace HOM
{
    public class SkillManager : MonoBehaviour
    {
        static SkillManager self;       //Singleton reference

        [Header("Settings"), Space(10)]
        [SerializeField] DashData dashData;

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
            Debug.LogWarning("Add Pickup Skill");
            Debug.LogWarning("Add Drop Skill");
            Debug.LogWarning("Add Throw Skill");
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

    }
}