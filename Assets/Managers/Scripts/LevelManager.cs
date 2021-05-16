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
        Animator ui_Animator;

        #region UnityCallbacks
        private void Awake()
        {
            Init();
        }
        #endregion

        void Init()
        {
            self = this;
            DontDestroyOnLoad(this);
            ui_Animator = GameObject.Find("UI_Manager").GetComponent<Animator>();
        }

        public static void LoadLevel(string levelName)
        {
            switch(levelName)
            {
                case "Main Menu":
                    self.StartCoroutine(self.ExecuteLevelTransition(self.loadingDuration, 0));
                    break;
                case "Blockout":
                    self.StartCoroutine(self.ExecuteLevelTransition(self.loadingDuration, 1));
                    break;
            }    
        }

        public void OnLevelWasLoaded(int level)
        {
            if (level == 0)
            {
                ui_Animator.SetInteger("Index", 0);
                currentIndex = 0;
            }
            else
            {
                ui_Animator.SetInteger("Index", 1);
                currentIndex = 1;
            }
        }

        IEnumerator ExecuteLevelTransition(float duration, int levelIndex)
        {
            loadingScreen.SetActive(true);
            isLoading = true;
            yield return new WaitForSeconds(1);
            yield return new WaitForSeconds(duration);
            SceneManager.LoadScene(levelIndex);
            loadingScreen.GetComponent<Animator>().SetTrigger("Out");
            yield return new WaitForSeconds(1);
            loadingScreen.SetActive(false);
            isLoading = false;
            ui_Animator.SetTrigger("SceneChanged");
        }
    }
}
