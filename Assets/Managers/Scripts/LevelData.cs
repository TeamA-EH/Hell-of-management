using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    [CreateAssetMenu(fileName = "New Levels Data", menuName = "Hell Of Management/Data/Levels Data")]
    public sealed class LevelData : ScriptableObject
    {
        [System.Serializable]
        public struct Level
        {
            public string levelName;
            public string levelGoalDescription;
            public string LevelLogoPath;

            public uint firstStarScore;
            public uint secondStarScore;
            public uint thirdStarScore;
        }

        [SerializeField] Level[] levels;

        public Level GetLevel(uint id) => levels[id];
    }
}
