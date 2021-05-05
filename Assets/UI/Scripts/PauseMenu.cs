using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HOM;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// This is used by the play button to resume the game.
    /// </summary>
    public void Resume()
    {
        
    }

    /// <summary>
    /// This is used by the settings button to open the settings.
    /// </summary>
    public void GoToSettings()
    {
        GUIHandler.ActivatesMenu("Settings Menu");
        GUIHandler.DeactivatesMenu("Pause Menu");
    }

    /// <summary>
    /// This is used by the controls button to open the controls.
    /// </summary>
    public void GoToControls()
    {
        GUIHandler.ActivatesMenu("Controls");
        GUIHandler.DeactivatesMenu("Pause Menu");
    }

    /// <summary>
    /// This is used by the credits button to open the credits.
    /// </summary>
    public void GoToCredits()
    {
        GUIHandler.ActivatesMenu("GoToCredits");
        GUIHandler.DeactivatesMenu("Pause Menu");
    }

    /// <summary>
    /// This is used by the quit button to open the quit menu.
    /// </summary>
    public void GoToQuitMenu()
    {
        Time.timeScale = 1;
        GUIHandler.DeactivatesMenu("Pause Menu");
        LevelManager.LoadLevel("Main Menu");
    }
}
