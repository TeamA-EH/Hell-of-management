using System;
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
        [SerializeField] GameObject ReviewMenuAsset;
        [SerializeField] GameObject UpgradeMenuAsset;
        [SerializeField] GameObject DayReplayMenuAsset;

        Dictionary<string, GameObject> m_menuesMap = new Dictionary<string, GameObject>();
        static GUIModule self = null;

        #region UnityCallbacks
        private void Start()
        {
            DontDestroyOnLoad(this);

            Initialize();
        }
        #endregion

        void Initialize()
        {
            /* Adds menues to the collection */
            m_menuesMap.Add("Main Menu", Instantiate(MainMenuAsset, gameObject.transform));
            m_menuesMap.Add("Settings Menu", Instantiate(SettingsMenuAsset, gameObject.transform));
            m_menuesMap.Add("Commands Menu", Instantiate(ControlsMenuAsset, gameObject.transform));
            m_menuesMap.Add("Credits Menu", Instantiate(CreditsMenuAsset, gameObject.transform));
            m_menuesMap.Add("Pause Menu", Instantiate(PauseMenuAsset, gameObject.transform));
            m_menuesMap.Add("Game Quit Popup", Instantiate(QuitMenuAsset, gameObject.transform));
            m_menuesMap.Add("Review Menu", Instantiate(ReviewMenuAsset, gameObject.transform));
            m_menuesMap.Add("Day Replay Popup", Instantiate(DayReplayMenuAsset, gameObject.transform));
            
            self = this;
        }

        /// <summary>
        /// Returns all GUI Module interfaces
        /// </summary>
        internal static Dictionary<string, GameObject> GetMenues() => self.m_menuesMap;

    }
}
