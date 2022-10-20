using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int score;
    int highScore;
    [SerializeField] NewHighScore newHighScore;
    Score[] ScoreTexts;
    // Start is called before the first frame update
    void Start()
    {
        ScoreTexts = FindObjectsOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int amount)
    {
       
        score += amount;
        if (score > PlayerPrefs.GetInt("highScore"))
        {
            highScore = score;
            newHighScore.SetActiveText(true);
            PlayerPrefs.SetInt("highScore", highScore);
        }
        foreach (Score s in ScoreTexts)
        {
            s.SetScore(score);
        }
    }


}
