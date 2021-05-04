using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace HOM
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager self;
        public GameObject loadingScreen;
        public bool isLoading;
        public float loadingDuration;
        public int currentIndex;

        public enum SceneIndexes { PersistentScene = 0, MainMenu = 1, Blockout = 2 };

        #region UnityCallbacks
        private void Awake()
        {
            Init();
            FirstScene();
        }
        #endregion

        void Init()
        {
            self = this;
        }

        /// <summary>
        /// This is the first scene when the game is booted up.
        /// </summary>
        void FirstScene()
        {
            SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu, LoadSceneMode.Additive);
            currentIndex = (int)SceneIndexes.MainMenu;
        }

        List<AsyncOperation> sceneLoading = new List<AsyncOperation>();

        /// <summary>
        /// This is called from the MainMenu start game button, to start the game.
        /// </summary>
        public void LoadGame()
        {
            loadingScreen.SetActive(true);
            isLoading = true;
            sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MainMenu));
            sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Blockout, LoadSceneMode.Additive));
            currentIndex = (int)SceneIndexes.Blockout;

            StartCoroutine(GetSceneLoadProgress(loadingDuration));
        }

        /// <summary>
        /// This is called from the PauseMenu quit game button, to return the MainMenu;
        /// </summary>
        public void LoadMainMenu()
        {
            loadingScreen.SetActive(true);
            isLoading = true;
            sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Blockout));
            sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu, LoadSceneMode.Additive));
            currentIndex = (int)SceneIndexes.MainMenu;


            StartCoroutine(GetSceneLoadProgress(loadingDuration));
        }

        /// <summary>
        /// This is used to give a timer to the loading screen, a customizable duration from inspector.
        /// </summary>
        float totalSceneProgress;
        public IEnumerator GetSceneLoadProgress(float duration)
        {
            for(int i=0; i<sceneLoading.Count; i++)
            {
                while(!sceneLoading[i].isDone)
                {
                    yield return new WaitForSeconds(duration);
                }
            }
            loadingScreen.SetActive(false);
            isLoading = false;
        }
    }
}
