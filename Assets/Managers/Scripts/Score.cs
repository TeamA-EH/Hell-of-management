using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score self;
    public float targetProgress = 0;
    public float enoughToWin;

    void Start()
    {
        Init();
    }

    void Init()
    {
        self = this;
    }
}
