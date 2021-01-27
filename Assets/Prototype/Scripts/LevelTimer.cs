using UnityEngine;
using System.Collections;

public class LevelTimer : MonoBehaviour
{
   [SerializeField] float levelTimeInMinutes = 2.5f;
    public float delayTime => levelTimeInMinutes * 60;
    public float currentTime { private set; get; }
    bool isActive => (currentTime <= delayTime);

    public delegate void TimerEventHandler(LevelTimer sender, int time);
    public static TimerEventHandler OnTimeChanges;

    private void Start()
    {
        currentTime = 0;
    }

    private void Update()
    {
        if(isActive)
        {
            currentTime += Time.deltaTime;
            OnTimeChanges?.Invoke(this, (int)currentTime);
        }
    }
}