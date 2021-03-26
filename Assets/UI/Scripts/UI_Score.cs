using UnityEngine;
using UnityEngine.UI;

public class UI_Score : MonoBehaviour
{
    Slider slider;
    static int scoreInt = 0;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        Stars();
        if (Input.GetKeyDown("space"))
        {
            IncrementProgress(0.10f);
        }
    }

    public void IncrementProgress(float newProgress)
    {
        Score.self.targetProgress = slider.value += newProgress;
    }

    public void Stars()
    {
        if (Score.self.targetProgress >= Score.self.enoughToWin)
            star1.GetComponent<Image>().color = new Color(1, 1, 1);

        if (Score.self.targetProgress >= 0.75f)
            star2.GetComponent<Image>().color = new Color(1, 1, 1);

        if (Score.self.targetProgress >= 1)
            star3.GetComponent<Image>().color = new Color(1, 1, 1);
    }
}
