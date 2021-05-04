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
        public float loadingDuration;

        public enum SceneIndexes { PersistentScene = 0, MainMenu = 1, Blockout = 2 };

        private void Awake()
        {
            instance = this;

            SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu, LoadSceneMode.Additive);
        }

        List<AsyncOperation> sceneLoading = new List<AsyncOperation>();

        public void LoadGame()
        {
            loadingScreen.SetActive(true);
            sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MainMenu));
            sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Blockout, LoadSceneMode.Additive));
           
            StartCoroutine(GetSceneLoadProgress(loadingDuration));
        }

        public void LoadMainMenu()
        {
            loadingScreen.SetActive(true);
            sceneLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Blockout));
            sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu, LoadSceneMode.Additive));
            

            StartCoroutine(GetSceneLoadProgress(loadingDuration));
        }

        private void Update()
        {
            Debug.Log(sceneLoading.Count);
        }

        float totalSceneProgress;
        public IEnumerator GetSceneLoadProgress(float duration)
        {
            for(int i=0; i<sceneLoading.Count; i++)
            {
                while(!sceneLoading[i].isDone)
                {
                    //yield return null;
                    yield return new WaitForSeconds(duration);
                }
            }
            loadingScreen.SetActive(false);
        }
    }
}
