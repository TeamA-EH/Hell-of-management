using UnityEngine;

namespace HOM
{
    [CreateAssetMenu(fileName = "New Dash Data", menuName = "Hell Of Management/Data/Dash Data")]
    public class DashData : ScriptableObject
    {
        [Tooltip("Represents the amount of time in seconds where the skill is active")]
        [SerializeField] [Range(0.1f, 3600)] float executionTime;
        [Tooltip("Represents the amount of time in seconds where the skill is unvailable")]
        [SerializeField] [Range(0.1f, 3600)] float cooldownTime;
        [Tooltip("Indica in quanto tempo un personaggio raggiunge una determinata velocita; NB: il punto finale deve avere lo stesso valore di [executionTime]!")]
        [SerializeField] AnimationCurve speedTweenin;

        /* DATA GETTERS */
        public float m_executionTime => executionTime;
        public float m_cooldownTime =>cooldownTime;
        public AnimationCurve m_speedTweening => speedTweenin;


    }
}