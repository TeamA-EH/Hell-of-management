using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }


    public int startingScore;
    [SerializeField] int currentscore;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        currentscore = startingScore;
    }
    
    /// <summary>
    /// This fun
    /// </summary>
    /// <param name="removePointsAmount"></param>
    public void RemovePoints(int removePointsAmount)
    {
        /* if Player does something wrong */
        currentscore -= removePointsAmount;
    }

    public void AddPoints(int addPointsAmount)
    {
        /* if player does something right */
        currentscore += addPointsAmount;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            AddPoints(5);

        if (Input.GetMouseButtonDown(1))
            RemovePoints(5);
    }

}
