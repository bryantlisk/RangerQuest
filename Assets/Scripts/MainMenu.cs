using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Canvas mainUI;
    [SerializeField] Canvas howToPlayUI;
    public void Quit()
    {
        Debug.Log("dorcasland lappdumb");
        Application.Quit();
    }

    private void Start()
    {
        mainUI.enabled = true;
        howToPlayUI.enabled = false;
    }

    public void StartArena()
    {
        SceneManager.LoadScene("Arena01");
    }

    public void StartMainGame()
    {
        Debug.Log("Not implemented yet");
    }

    public void HowToPlay()
    {
        mainUI.enabled = false;
        howToPlayUI.enabled = true;
    } 

    public void BackToMain()
    {
        mainUI.enabled = true;
        howToPlayUI.enabled = false;
    }
}
