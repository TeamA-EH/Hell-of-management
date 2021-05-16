using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class MainMenu : MonoBehaviour
    {
        Animator animator;

        public void Start()
        {
            animator = GameObject.Find("UI_Manager").GetComponent<Animator>();
        }
        /// <summary>
        /// This is used by the play button to start the game.
        /// </summary>
        public void PlayGame()
        {
            animator.SetTrigger("StartGame");
        }

        /// <summary>
        /// This is used by the settings button to open the settings.
        /// </summary>
        public void GoToSettings()
        {
            animator.SetTrigger("Settings");
        }

        /// <summary>
        /// This is used by the controls button to open the controls.
        /// </summary>
        public void GoToControls()
        {
            //animator.SetTrigger("Controls");
        }

        /// <summary>
        /// This is used by the credits button to open the credits.
        /// </summary>
        public void GoToCredits()
        {
            animator.SetTrigger("Credits");
        }

        /// <summary>
        /// This is used by the quit button to open the quit menu.
        /// </summary>
        public void GoToQuitMenu()
        {
            animator.SetTrigger("QuitGame");
        }
    }
}
