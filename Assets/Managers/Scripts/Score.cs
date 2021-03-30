using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score self;
    public float targetProgress = 0;

    void Start()
    {
        Init();
    }

    void Init()
    {
        self = this;
    }
}
