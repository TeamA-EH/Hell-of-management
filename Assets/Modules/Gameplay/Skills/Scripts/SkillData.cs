using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    [CreateAssetMenu(fileName = "New Skill Data", menuName = "Hell Of Management/Data/Skill Data")]
    public class SkillData : ScriptableObject
    {
        [Header("Settings"), Space(10)]
        [SerializeField] [Range(0.1f, 250)] float maxSpeed = 1;

        /* SKILL GETTERS */
        ///<summary> Returns the max speed for this skill </summary>
        public float MaxSpeed => maxSpeed;
        ///<summary> Return the velocity curve for this skill </summary>
    }
}
