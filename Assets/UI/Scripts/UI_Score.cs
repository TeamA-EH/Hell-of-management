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
    [SerializeField] GameObject scoreNumber;
    [SerializeField] GameObject scoreBar;
    private bool isActive = false;
    private bool isPaused = false;

    #region Unity Callbacks
    public void Awake()
    {
        Init();
    }
    void OnDisable()
    {
        Score.OnProgressChanged -= UpdateGraphics;
    }

    void Update()
    {
        Activate();
    }
    #endregion

    void Init()
    {
        /* RESETS GFX */
        fillImg.fillAmount = 0;
        scoreText.text = $"Score: {0}";

        Score.OnProgressChanged+=UpdateGraphics;
    }

    void Activate()
    {
        if (LevelManager.self != null)
        {
            if (!LevelManager.self.isLoading && LevelManager.self.currentIndex == 1 && isActive == false)
            {
                scoreBar.SetActive(true);
                scoreNumber.SetActive(true);
                isActive = true;
            }
            if (Time.timeScale == 0 && isPaused == false)
            {
                scoreBar.SetActive(false);
                scoreNumber.SetActive(false);
                isPaused = true;
            }
            if (Time.timeScale == 1 && isPaused == true)
            {
                scoreBar.SetActive(true);
                scoreNumber.SetActive(true);
                isPaused = false;
            }
        }
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

        else if (score > levelData.GetLevel(0).firstStarScore && score <= levelData.GetLevel(0).secondStarScore)
        {
            scoreText.text = "Score: " + score;
            fillImg.fillAmount = score / (float)levelData.GetLevel(0).thirdStarScore;

            if (score >= levelData.GetLevel(0).secondStarScore)
            {
                LightStar(ref star1, Color.white);
                LightStar(ref star2, Color.white);
            }

            return;
        }

        else if (score > 750 && score <= levelData.GetLevel(0).thirdStarScore)
        {
            scoreText.text = "Score: " + score;
            fillImg.fillAmount = score / (float)levelData.GetLevel(0).thirdStarScore;

            if(score >= (float)levelData.GetLevel(0).thirdStarScore)
            {
                LightStar(ref star1, Color.white);
                LightStar(ref star2, Color.white);
                LightStar(ref star3, Color.white);
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
