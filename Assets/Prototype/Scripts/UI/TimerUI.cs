using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] Text timeText;

    #region UnityCallbacks
    private void OnEnable()
    {
        LevelTimer.OnTimeChanges += UpdateTimeString;
    }
    private void OnDisable()
    {
        LevelTimer.OnTimeChanges -= UpdateTimeString;
    }
    #endregion

    protected void UpdateTimeString(LevelTimer sender, int value)
    {
        timeText.text = $"Time: {sender.delayTime - value}";
    }
}