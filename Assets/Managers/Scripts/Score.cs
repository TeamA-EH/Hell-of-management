using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score self;
    public float targetProgress {private set; get;} = 0;

    #region Events
    public static event Action<Score, float> OnProgressChanged;
    #endregion
    void Start()
    {
        Init();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            AddScore(500);
        }
    }

    void Init()
    {
        self = this;
    }

    public void AddScore(float score)
    {
        targetProgress = Mathf.Clamp(targetProgress + score, 0, 66666666);
        OnProgressChanged?.Invoke(this, targetProgress);
    }
}
