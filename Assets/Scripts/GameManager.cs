using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    bool pausingEnabled;
    [SerializeField] Canvas gameOverScreen;
    [SerializeField] Canvas mainUI;
    [SerializeField] Canvas pauseUI;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pausingEnabled = true;
        mainUI.enabled = true;
        gameOverScreen.enabled = false;
        pauseUI.enabled = false;
        Physics2D.IgnoreLayerCollision(7, 7, true);
        //Physics2D.IgnoreLayerCollision(6, 8);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;
        if (pausingEnabled)
        {
            if (isPaused)
            {
                Time.timeScale = 0;
                pauseUI.enabled = true;
            }
            if (!isPaused)
            {
                pauseUI.enabled = false;
                Time.timeScale = 1;
            }
        }
    }

    public void Pause(bool b)
    {
        isPaused = b;
        if (pausingEnabled)
        {
            if (isPaused)
            {
                Time.timeScale = 0;
                pauseUI.enabled = true;
            }
            if (!isPaused)
            {
                pauseUI.enabled = false;
                Time.timeScale = 1;
            }
        }
    }


    public void GameOver()
    {
        Time.timeScale = 0;
        isPaused = true;
        mainUI.enabled = false;
        gameOverScreen.enabled = true;


    }

    public void ResetGame()
    {
        Debug.Log("Skippidy bop bup baddum");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToDesktop()
    {
        Debug.Log("dorcasland lappdumb");
        Application.Quit();
    }

    public void ExitToMenu()
    {
        Debug.Log("trollers of romania");
        SceneManager.LoadScene("MainMenu");
    }
}
