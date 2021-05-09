using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class SettingsMenu : MonoBehaviour
    {

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
            GUIHandler.DeactivatesMenu("Settings Menu");
            GUIHandler.ActivatesMenu("Main Menu");
        }
    }
}
