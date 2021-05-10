using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class CreditsMenu : MonoBehaviour
    {
        public void Back()
        {
            if (LevelManager.self.currentIndex == 0)
            {
                GUIHandler.DeactivatesMenu("Credits Menu");
                GUIHandler.ActivatesMenu("Main Menu");
            }
            else if (LevelManager.self.currentIndex == 1)
            {
                GUIHandler.DeactivatesMenu("Credits Menu");
                GUIHandler.ActivatesMenu("Pause Menu");
            }
        }
    }
}
