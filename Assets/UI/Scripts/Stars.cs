using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour
{
    public Slider slider;
    public float sliderAmount;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    // Update is called once per frame
    void Update()
    {
        sliderAmount = slider.value;

        if (sliderAmount >= 0.5f)
            star1.GetComponent<Image>().color = new Color(1, 1, 1);

        if (sliderAmount >= 0.75f)
            star2.GetComponent<Image>().color = new Color(1, 1, 1);

        if (sliderAmount >= 1)
            star3.GetComponent<Image>().color = new Color(1, 1, 1);
    }
}
