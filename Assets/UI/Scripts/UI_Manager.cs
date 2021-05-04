using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class UI_Manager : MonoBehaviour
    {
        static UI_Manager self;
        static bool GameIsPaused = false;
        int memorizedIndex;

        #region UnityCallbacks

        void Start()
        {
            Init();
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
        }

        /// <summary>
        /// This checks the current scene and if it was changed..
        /// </summary>
        void CheckScene()
        {
            if (!LevelManager.self.isLoading && LevelManager.self.currentIndex == 1 && memorizedIndex != LevelManager.self.currentIndex)
            {
                OpenMainMenu();
                memorizedIndex = LevelManager.self.currentIndex;
            }
            else if (!LevelManager.self.isLoading && LevelManager.self.currentIndex == 2 && memorizedIndex != LevelManager.self.currentIndex)
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
            //GUIHandler.ActivatesMenu("DialogueBox");
            GUIHandler.ActivatesMenu("Score");
            GUIHandler.ActivatesMenu("Throw Drop");
            GUIHandler.ActivatesMenu("ToDoList");
        }

        /// <summary>
        /// This is used to check if the game is paused or resumed.
        /// </summary>
        void CheckStatus()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !LevelManager.self.isLoading && LevelManager.self.currentIndex == 2)
            {
                if (GameIsPaused)
                    Resume();
                else
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
            //GUIHandler.ActivatesMenu("DialogueBox");
            GUIHandler.ActivatesMenu("Score");
            GUIHandler.ActivatesMenu("Throw Drop");
            GUIHandler.ActivatesMenu("ToDoList");
            Time.timeScale = 1;
            GameIsPaused = false;
        }

        /// <summary>
        /// This is used to pause the game.
        /// </summary>
        void Pause()
        {
            GUIHandler.ActivatesMenu("Pause Menu");
            GUIHandler.DeactivatesMenu("Clock");
            //GUIHandler.DeactivatesMenu("DialogueBox");
            GUIHandler.DeactivatesMenu("Score");
            GUIHandler.DeactivatesMenu("Throw Drop");
            GUIHandler.DeactivatesMenu("ToDoList");
            Time.timeScale = 0;
            GameIsPaused = true;
        }
    }
}