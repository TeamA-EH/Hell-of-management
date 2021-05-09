using System;
using UnityEngine;

namespace HOM
{
    public class SkillUsageManager : MonoBehaviour
    {
        public static SkillUsageManager self { private set; get; } = null;

        public enum SelectedType { NONE = -1, DROP, THROW }
        ///<summary> Repesent the modality for use an item; possible valie: DROP or Throw </summary>
        SelectedType itemMod = SelectedType.THROW;
        public SelectedType ItemMod
        {
            get => itemMod;
            private set
            {
                itemMod = value;
            }
        }

        #region Events
        ///<summary> Event called when the items usage changes </summary>
        public static event Action<SelectedType> OnModChanged;
        #endregion

        #region Unity Callbacks
        void Start()
        {
            if (!self) self = this;  // singleton reference
        }
        /* void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                //SwapItemUsage();
            }
        }*/
        #endregion

        void SwapItemUsage()
        {
            switch(itemMod)
            {
                case SelectedType.DROP:
                    ItemMod = SelectedType.THROW;
                break;
                case SelectedType.THROW:
                    ItemMod = SelectedType.DROP;
                break;
            }
        }
    }
}
