using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tasks : MonoBehaviour
{
    public bool taskFinished = false;
    public Sprite unticked;
    public Sprite ticked;
    public GameObject taskbox;

    void Update()
    {
        if(taskFinished)
            taskbox.GetComponent<Image>().sprite = ticked;
        else
            taskbox.GetComponent<Image>().sprite = unticked;
    }
}
