using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [HideInInspector] public TextMeshProUGUI text;
    int score;
    [HideInInspector] public LevelController levelController;
    [SerializeField] bool isHighScore;
    // Start is called before the first frame update
    void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        levelController = FindObjectOfType<LevelController>();
        score = 0;
        if (isHighScore)
        {
            text.text = "High score: " + PlayerPrefs.GetInt("highScore");
        }
        else
        {
            text.text = "Score: " + score;
        }

        
    }


    public void SetScore(int amount)
    {
        if (isHighScore)
        {
            text.text = "High score: " + PlayerPrefs.GetInt("highScore");

        }
        else
        {
            score = amount;
            text.text = "Score: " + score; 
        }

    }

}
