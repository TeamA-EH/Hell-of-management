using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOM;

public class QuitMenu : MonoBehaviour
{
    public void GoBack()
    {
        GUIHandler.DeactivatesMenu("Quit Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
