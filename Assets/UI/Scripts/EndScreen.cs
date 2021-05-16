using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class EndScreen : MonoBehaviour
    {
        Animator animator;
        public void Start()
        {
            animator = GameObject.Find("UI_Manager").GetComponent<Animator>();
        }

        public void Restart()
        {
            animator.SetTrigger("EndGame");
            LevelManager.LoadLevel("Blockout");
        }

        public void GoToMainMenu()
        {
            animator.SetTrigger("EndGame");
            LevelManager.LoadLevel("Main Menu");
        }
    }
}
