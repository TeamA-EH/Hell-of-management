using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public static Timer self;
    public float totaltime = 60;
    public float currentTime;
    public bool runTimer => currentTime > 0;



    public static event Action<Timer, float>OnEndTimer;

    private void Start()
    {
        Init();
        currentTime = totaltime;
    }

    void Init()
    {
        self = this;
    }

    void Update()
    {
        TimerGoingDown();
    }

    void TimerGoingDown()
    {
        if (runTimer)
        {
            currentTime = Mathf.Clamp(currentTime - Time.deltaTime, 0, currentTime);
            if (currentTime == 0)
                OnEndTimer?.Invoke(this, currentTime);
        }
    }
}
