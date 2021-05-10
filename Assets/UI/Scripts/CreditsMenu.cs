using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class CreditsMenu : MonoBehaviour
    {
        public void Back()
        {
            GUIHandler.DeactivatesMenu("Credits Menu");
            GUIHandler.ActivatesMenu("Main Menu");
        }
    }
}
