using UnityEngine;
using UnityEngine.UI;

public class UI_Clock : MonoBehaviour
{
    private Image circle;

    private void Start()
    {
        circle = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        UITimerGoingDown();
    }

    void UITimerGoingDown()
    {
        if (Timer.self.runTimer)
        {
            circle.fillAmount = Mathf.Clamp(Timer.self.currentTime / Timer.self.totaltime, 0, 1);
        }
    }
}
