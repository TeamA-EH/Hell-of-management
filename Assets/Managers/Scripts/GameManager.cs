using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOM;

[RequireComponent(typeof (Animator))]
public class GameManager : MonoBehaviour
{
    Animator animator;
    Animator ui_animator;
    static GameManager self;

    [SerializeField] LevelData levelsData;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        self = this;
        Timer.OnEndTimer += OnEndTimer;
        animator = gameObject.GetComponent<Animator>();
        ui_animator = GameObject.Find("UI_Manager").GetComponent<Animator>();
    }

    void OnEndTimer(Timer sender, float currentTime)
    {
        if (Score.self.targetProgress >= levelsData.GetLevel(0).firstStarScore)
                WinLevel();
        else
            LoseLevel();
    }


    public void LoseLevel()
    {
        if(!LevelManager.self.isLoading)
            ui_animator.SetTrigger("Defeat");
    }

    public void WinLevel()
    {
        if (!LevelManager.self.isLoading)
            ui_animator.SetTrigger("Defeat");
    }
}
