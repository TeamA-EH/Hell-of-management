using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class UI_Manager : MonoBehaviour
    {
        static UI_Manager self;

        bool sceneHasChanged;
        public float checkScene;

        void Start()
        {
            Init();
        }

        void Init()
        {
            self = this;
            DontDestroyOnLoad(this);
            //StartCoroutine(Alessiomiuccide(0.1f));
        }

        IEnumerator Alessiomiuccide(float time)
        {
            yield return new WaitForSeconds(time);

            //checkScene = LevelManager.self.currentScene;
            if (checkScene == 0)
                OpenMainMenu();
            else if (checkScene == 1)
                OpenHud();
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
        
        void LevelCheck()
        {
            //if (checkScene != LevelManager.self.currentScene)
            //{
            //    sceneHasChanged = true;
            //    if (sceneHasChanged == true && LevelManager.self.currentScene == 0)
            //    {
            //        OpenMainMenu();
            //        checkScene = LevelManager.self.currentScene;
            //        sceneHasChanged = false;
            //    }
            //    else if (sceneHasChanged == true && LevelManager.self.currentScene == 1)
            //    {
            //        OpenHud();
            //        checkScene = LevelManager.self.currentScene;
            //        sceneHasChanged = false;
            //    }
            //}
        }

        //void Update()
        //{
        //    LevelCheck();
        //}
    }
}