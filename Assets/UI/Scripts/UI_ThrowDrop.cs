using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HOM;

public class UI_ThrowDrop : MonoBehaviour
{
    [SerializeField] Sprite _throw;
    [SerializeField] Sprite _drop;
    [SerializeField] Image _status;
    [SerializeField] Text throwdropText;

    public float timeAfterFade;
    private float currentTimeAfterFade;
    private Sprite currentSprite;
    private bool fade;

    #region Unity Callbacks

    void Start()
    {
        currentSprite = _status.sprite;
        currentTimeAfterFade = timeAfterFade;
    }

    void Update()
    {
        CheckAction();
        FadeTimer();
        ChangeText();
        ChangeStatus();
    }
    #endregion

    /// <summary>
    /// This check if the sprite changes, so it can set it from transparent to opaque.
    /// </summary>
    void CheckAction()
    {
        if (currentSprite != _status.sprite)
        {
            currentSprite = _status.sprite;
            currentTimeAfterFade = timeAfterFade;
            fade = false;
            StartCoroutine(FadeImage(false));
        }
    }    

    /// <summary>
    /// This changes the text from DROP to THROW or from THROW to DROP.
    /// </summary>
    void ChangeText()
    {
        throwdropText.text = SkillUsageManager.self.ItemMod == SkillUsageManager.SelectedType.DROP ? "DROP" : "THROW";
    }

    /// <summary>
    /// This is used to change the sprite.
    /// </summary>
    void ChangeStatus()
    {
        if (SkillUsageManager.self.ItemMod == SkillUsageManager.SelectedType.DROP)
        {
            _status.sprite = _drop;
        }
        else
        {
            _status.sprite = _throw;
        }
    }

    /// <summary>
    /// This is a timer for the fade effect on the sprite.
    /// </summary>
    void FadeTimer()
    {
        if (currentTimeAfterFade > 0)
        {
            currentTimeAfterFade -= Time.deltaTime;
        }

        if(currentTimeAfterFade <= 0 && fade == false)
        {
            StartCoroutine(FadeImage(true));
            fade = true;
        }
    }

    /// <summary>
    /// This is the fade effect on the sprite.
    /// </summary>
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                _status.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
                _status.color = new Color(1, 1, 1, 1);
                yield return null;
        }
    }
}
