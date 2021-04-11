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

    #region Unity Callbacks
    public void Awake()
    {
        Init();
    }
    void OnDisable()
    {
        Score.OnProgressChanged -= UpdateGraphics;
    }
    #endregion

    void Init()
    {
        /* RESETS GFX */
        fillImg.fillAmount = 0;
        scoreText.text = $"Score: {0}";

        Score.OnProgressChanged+=UpdateGraphics;
    }

    public void UpdateGraphics(Score sender, float score)
    {
        if(score <= levelData.GetLevel(0).firstStarScore)
        {
            scoreText.text = "Score: " + score;
            fillImg.fillAmount = score / (float)levelData.GetLevel(0).thirdStarScore;
            
            if(score >= (float)levelData.GetLevel(0).firstStarScore)
            {
                LightStar(ref star1, Color.white);
            }

            return;
        }

        else if (score <= levelData.GetLevel(0).thirdStarScore)
        {
            scoreText.text = "Score: " + score;
            fillImg.fillAmount = score / (float)levelData.GetLevel(0).thirdStarScore;

            if(score >= (float)levelData.GetLevel(0).thirdStarScore)
            {
                LightStar(ref star3, Color.white);
            }
            //This is spaghetti code.. (non prende dal level data il second star code)
            if(score >= 750)
            {
                LightStar(ref star2, Color.white);
            }

            return;
        }

        else if (score > levelData.GetLevel(0).thirdStarScore)
        {
            scoreText.text = "Score: " + score;
            return;
        }
    }

    public void LightStar(ref Image star, Color color) => star.color = color;
}
