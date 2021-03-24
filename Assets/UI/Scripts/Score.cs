using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Slider slider;

    private float targetProgress = 0;

    public void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
            IncrementProgress(0.10f);
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value += newProgress;
    }
}
