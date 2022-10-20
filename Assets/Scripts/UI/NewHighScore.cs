using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewHighScore : MonoBehaviour
{
    TextMeshProUGUI text;
    LevelController levelController;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        levelController = FindObjectOfType<LevelController>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveText(bool b)
    {
        text.enabled = b;
    }

}
