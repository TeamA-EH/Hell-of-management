using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOM;

public class QuitMenu : MonoBehaviour
{
    Animator animator;

    public void Start()
    {
        animator = GameObject.Find("UI_Manager").GetComponent<Animator>();
    }

    public void GoBack()
    {
        animator.SetTrigger("Back");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
