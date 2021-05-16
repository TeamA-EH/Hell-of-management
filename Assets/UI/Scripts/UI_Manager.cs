using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class UI_Manager : MonoBehaviour
    {
        public static UI_Manager self;
        Animator animator;

        #region UnityCallbacks

        void Start()
        {
            Init();
            DontDestroyOnLoad(this);
        }

        void Update()
        {
            CheckStatus();
        }

        #endregion

        void Init()
        {
            self = this;
            animator = gameObject.GetComponent<Animator>();
            animator.SetInteger("Index", LevelManager.self.currentIndex);
        }

        /// <summary>
        /// This is used to check if the game is paused or resumed.
        /// </summary>
        void CheckStatus()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !LevelManager.self.isLoading)
            {
                if (animator.GetBool("Paused"))
                {
                    animator.SetBool("Paused", false);
                }
                else if (!animator.GetBool("Paused"))
                {
                    animator.SetBool("Paused", true);
                }
                PauseGame();
            }
        }

        void PauseGame()
        {
            if (animator.GetBool("Paused"))
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}