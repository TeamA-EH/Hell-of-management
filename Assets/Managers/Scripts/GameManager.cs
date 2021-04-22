using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOM;

[RequireComponent(typeof (Animator))]
public class GameManager : MonoBehaviour
{
    Animator animator;
    static GameManager self;

    [SerializeField] LevelData levelsData;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        animator = gameObject.GetComponent<Animator>();
        self = this;
        Timer.OnEndTimer += OnEndTimer;
    }

    void OnEndTimer(Timer sender, float currentTime)
    {
        if (Score.self.targetProgress >= levelsData.GetLevel(0).firstStarScore)
            WinLevel();
        else
            LoseLevel();
    }


    public static void LoseLevel()
    {
        self.animator.SetTrigger("Defeat");
        Debug.Log("Lost");
    }

    public static void WinLevel()
    {
        self.animator.SetTrigger("Victory");
        Debug.Log("Won");
    }
}
