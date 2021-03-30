using UnityEngine;
using UnityEngine.UI;
using HOM;

public class UI_Score : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    [SerializeField] Text scoreText;
    [SerializeField] Image star1;
    [SerializeField] Image star2;
    [SerializeField] Image star3;
    [SerializeField] Image fillImg;

    public void Awake()
    {
        fillImg.fillAmount = 0;
        scoreText.text = $"Score: {0}";
    }

    private void Update()
    {
        LightStar();
        if (Input.GetKeyDown("space"))
        {
            IncreaseBarAmout();
        }
    }

    public void IncreaseBarAmout()
    {
        float progress = Score.self.targetProgress;
        float amount2Increase = 0.0f;

        if(progress < levelData.GetLevel(0).firstStarScore)
        {
            amount2Increase = 20 / (float)levelData.GetLevel(0).thirdStarScore;
            Score.self.targetProgress += 20;
            scoreText.text = "Score: " + Score.self.targetProgress;
            fillImg.fillAmount = Score.self.targetProgress / (float)levelData.GetLevel(0).thirdStarScore;
            return;
        }
        else if(progress >= levelData.GetLevel(0).firstStarScore && progress < levelData.GetLevel(0).secondStarScore)
        {
            amount2Increase = 5 / (float)levelData.GetLevel(0).thirdStarScore;
            Score.self.targetProgress += 5;
            scoreText.text = "Score: " + Score.self.targetProgress;
            fillImg.fillAmount = Score.self.targetProgress / (float)levelData.GetLevel(0).thirdStarScore;
            return;
        }
        else if (progress >= levelData.GetLevel(0).secondStarScore && progress < levelData.GetLevel(0).thirdStarScore)
        {
            amount2Increase = 2.5f / (float)levelData.GetLevel(0).thirdStarScore;
            Score.self.targetProgress += 2.5f;
            scoreText.text = "Score: " + Score.self.targetProgress;
            fillImg.fillAmount = Score.self.targetProgress / (float)levelData.GetLevel(0).thirdStarScore;
            return;
        }
    }

    public void LightStar()
    {
        if (Score.self.targetProgress >= (float)levelData.GetLevel(0).firstStarScore)
            star1.color = new Color(255, 255, 255);
        
        if (Score.self.targetProgress >= (float)levelData.GetLevel(0).secondStarScore)
            star2.color = new Color(255, 255, 255);

        if (Score.self.targetProgress >= (float)levelData.GetLevel(0).thirdStarScore)
            star3.color = new Color(255, 255, 255);
    }
}
