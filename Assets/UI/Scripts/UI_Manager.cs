using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class UI_Manager : MonoBehaviour
    {
        static UI_Manager self;
        public int memorizedIndex;

        #region UnityCallbacks

        void Start()
        {
            Init();
            DontDestroyOnLoad(this);
        }

        void Update()
        {
            CheckScene();
            CheckStatus();
        }

        #endregion

        void Init()
        {
            self = this;
            OpenMainMenu();
        }

        /// <summary>
        /// This checks the current scene and if it was changed..
        /// </summary>
        void CheckScene()
        {
            if (!LevelManager.self.isLoading && LevelManager.self.currentIndex == 0 && memorizedIndex != LevelManager.self.currentIndex)
            {
                OpenMainMenu();
                memorizedIndex = LevelManager.self.currentIndex;
            }
            else if (!LevelManager.self.isLoading && LevelManager.self.currentIndex == 1 && memorizedIndex != LevelManager.self.currentIndex)
            {
                OpenHud();
                memorizedIndex = LevelManager.self.currentIndex;
            }
        }
        
        /// <summary>
        /// This is used to open the main menu when its scene is loaded.
        /// </summary>
        void OpenMainMenu()
        {
            GUIHandler.ActivatesMenu("Main Menu");
        }

        /// <summary>
        /// This is used to open the hud when its scene is loaded.
        /// </summary>
        void OpenHud()
        {
            GUIHandler.ActivatesMenu("Clock");
            GUIHandler.ActivatesMenu("ToDoList");
        }

        /// <summary>
        /// This is used to check if the game is paused or resumed.
        /// </summary>
        void CheckStatus()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !LevelManager.self.isLoading)
            {
                if (Time.timeScale == 0)
                    Resume();
                else if(Time.timeScale == 1)
                    Pause();
            }
        }

        /// <summary>
        /// This is used to resume the game.
        /// </summary>
        void Resume()
        {
            GUIHandler.DeactivatesMenu("Pause Menu");
            GUIHandler.ActivatesMenu("Clock");
            GUIHandler.ActivatesMenu("ToDoList");
            Time.timeScale = 1;
        }

        /// <summary>
        /// This is used to pause the game.
        /// </summary>
        void Pause()
        {
            GUIHandler.ActivatesMenu("Pause Menu");
            GUIHandler.DeactivatesMenu("Clock");
            GUIHandler.DeactivatesMenu("ToDoList");
            Time.timeScale = 0;
        }
    }
}