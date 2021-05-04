using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class UI_Manager : MonoBehaviour
    {
        static UI_Manager self;
        public static bool GameIsPaused = false;
        public int memorizedIndex;

        void Start()
        {
            Init();
        }

        void Init()
        {
            self = this;
        }

        void CheckScene()
        {
            if (!LevelManager.instance.isLoading && LevelManager.instance.currentIndex == 1 && memorizedIndex != LevelManager.instance.currentIndex)
            {
                OpenMainMenu();
                memorizedIndex = LevelManager.instance.currentIndex;
            }
            else if (!LevelManager.instance.isLoading && LevelManager.instance.currentIndex == 2 && memorizedIndex != LevelManager.instance.currentIndex)
            {
                OpenHud();
                memorizedIndex = LevelManager.instance.currentIndex;
            }
        }
        
        void OpenMainMenu()
        {
            GUIHandler.ActivatesMenu("Main Menu");
            GUIHandler.DeactivatesMenu("Hud");
        }

        void OpenHud()
        {
            GUIHandler.DeactivatesMenu("Main Menu");
            GUIHandler.ActivatesMenu("Hud");
        }

        void Update()
        {
            CheckScene();
            CheckStatus();
        }

        void CheckStatus()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !LevelManager.instance.isLoading && LevelManager.instance.currentIndex == 2)
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        void Resume()
        {
            GUIHandler.DeactivatesMenu("Pause Menu");
            GUIHandler.ActivatesMenu("Hud");
            Time.timeScale = 1;
            GameIsPaused = false;
        }

        void Pause()
        {
            GUIHandler.ActivatesMenu("Pause Menu");
            GUIHandler.DeactivatesMenu("Hud");
            Time.timeScale = 0;
            GameIsPaused = true;
        }
    }
}