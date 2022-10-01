using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text scoreboard;
    public static ScoreBoard instance;
    void Awake()
    {
        
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }



    private void Start()
    {
        
        scoreboard = GetComponent<TMP_Text>();
        Debug.Log("Real score : " + score);
        
    }
    public void UpdateScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreboard.text = "SCORE : " + score.ToString();
    }
}
