using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image circle;
    public float totaltime = 60;

    void Update()
    {
        totaltime = totaltime - Time.deltaTime;
        //maxvalue/totaltime timer*famerate
        circle.fillAmount -= 1 / totaltime * Time.deltaTime;
    }
}
