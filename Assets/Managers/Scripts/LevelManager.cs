using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace HOM
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        public GameObject loadingScreen;
        public bool isLoading;
        public float loadingDuration;
        public int currentIndex;

        public enum SceneIndexes { PersistentScene = 0, MainMenu = 1, Blockout = 2 };

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this);
            SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu, LoadSceneMode.Additive);
            currentIndex = (int)SceneIndexes.MainMenu;
        }

        List<AsyncOperation> sceneLoading = new List<AsyncOperation>();

        public void LoadGame()
        {
            loadingScreen.SetActive(true);
            isLoading = true;
            sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MainMenu));
            sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Blockout, LoadSceneMode.Additive));
            currentIndex = (int)SceneIndexes.Blockout;

            StartCoroutine(GetSceneLoadProgress(loadingDuration));
        }

        public void LoadMainMenu()
        {
            loadingScreen.SetActive(true);
            isLoading = true;
            sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Blockout));
            sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu, LoadSceneMode.Additive));
            currentIndex = (int)SceneIndexes.MainMenu;


            StartCoroutine(GetSceneLoadProgress(loadingDuration));
        }

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
