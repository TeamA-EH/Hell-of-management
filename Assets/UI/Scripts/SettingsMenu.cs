using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class SettingsMenu : MonoBehaviour
    {
        Animator animator;

        public void Start()
        {
            animator = GameObject.Find("UI_Manager").GetComponent<Animator>();
        }

        public void Resolution()
        {
            
        }

        public void Graphics()
        {

        }

        public void WindowMode()
        {

        }

        public void Brightness()
        {

        }

        public void Volume()
        {

        }

        public void Back()
        {
                animator.SetTrigger("Back");
        }
    }
}
