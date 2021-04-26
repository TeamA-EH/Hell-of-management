using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    [CreateAssetMenu(fileName = "New Skill Data", menuName = "Hell Of Management/Data/Skill Data")]
    public class SkillData : ScriptableObject
    {
        [Header("Settings"), Space(10)]
        [Tooltip("Indica la distanza percorsa durante il lancio")]
        [SerializeField] float distance = 2;
        [Tooltip("Indica in quanto tempo viene effettuata l'azione")]
        [SerializeField] float flightTime = 1;

        /* SKILL GETTERS */
        /// <summary>
        /// Indica la distanza percorsa durante il lancio
        /// </summary>
        public float Distance => distance;
        /// <summary>
        /// Indica in quanto tempo viene effettuata l'azione
        /// </summary>
        public float FlightTime => flightTime;
    }
}
