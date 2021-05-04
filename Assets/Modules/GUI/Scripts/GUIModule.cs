using System.Collections.Generic;
using UnityEngine;

namespace HOM.Modules
{
    internal sealed class GUIModule : MonoBehaviour
    {
        [SerializeField] GameObject MainMenuAsset;
        [SerializeField] GameObject SettingsMenuAsset;
        [SerializeField] GameObject ControlsMenuAsset;
        [SerializeField] GameObject CreditsMenuAsset;
        [SerializeField] GameObject PauseMenuAsset;
        [SerializeField] GameObject QuitMenuAsset;
        [SerializeField] GameObject ClockAsset;
        [SerializeField] GameObject DialogueBoxAsset;
        [SerializeField] GameObject ScoreAsset;
        [SerializeField] GameObject ThrowDropAsset;
        [SerializeField] GameObject ToDoListAsset;


        Dictionary<string, GameObject> m_menuesMap = new Dictionary<string, GameObject>();
        static GUIModule self = null;

        #region UnityCallbacks
        private void Start()
        {
            //DontDestroyOnLoad(this);

            Initialize();

        }
        #endregion

        /// <summary> Initializes the menu resources for static draw </summary>
        void Initialize()
        {
            /* Adds menues to the collection */
            SetMenuResource("Main Menu", ref MainMenuAsset);
            SetMenuResource("Settings Menu", ref SettingsMenuAsset);
            SetMenuResource("Controls Menu", ref ControlsMenuAsset);
            SetMenuResource("Credits Menu", ref CreditsMenuAsset);
            SetMenuResource("Pause Menu", ref PauseMenuAsset);
            SetMenuResource("Quit Menu", ref QuitMenuAsset);
            SetMenuResource("Clock", ref ClockAsset);
            SetMenuResource("DialogueBox", ref DialogueBoxAsset);
            SetMenuResource("Score", ref ScoreAsset);
            SetMenuResource("Throw Drop", ref ThrowDropAsset);
            SetMenuResource("ToDoList", ref ToDoListAsset);

            self = this;
        }

        /// <summary>
        /// Returns all GUI Module interfaces
        /// </summary>
        internal static Dictionary<string, GameObject> GetMenues() => self.m_menuesMap;
        
        /// <summary> 
        /// Sets a link to the menues collection
        /// </summary>
        bool SetMenuResource(string menuName, ref GameObject asset)
        {
            if(asset)
            {
                var menu = Instantiate(asset, gameObject.transform);
                menu.SetActive(false);
                m_menuesMap.Add(menuName, menu);

            }
            else        // Error Handling
            {
                Debug.LogWarning($"{menuName} resource initialization failed!");
                return false;
            }

            return true;
        }

    }
}
