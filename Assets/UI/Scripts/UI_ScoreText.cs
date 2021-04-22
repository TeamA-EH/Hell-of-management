using UnityEngine.UI;
using UnityEngine;

public class UI_ScoreText : MonoBehaviour
{
    Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    public void Update()
    {
        _text.text = "Score:  " + Score.self.targetProgress;
    }
}
