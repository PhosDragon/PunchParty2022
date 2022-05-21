using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MainMenu
{
    public Canvas Canvas;
    public GameObject PausedUI;
    public GameObject OptionsUI;
    public PlayerControler Player;
    private bool isPaused;
    void Start()
    {
        
    }
    void Update()
    {
        PauseMenu(); 
    }

    public void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Pause();
        }
    }
    private void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        PausedUI.SetActive(true);
    }
    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1;
        PausedUI.SetActive(false);
    }
    public void ShowOptions()
    {
        OptionsUI.SetActive(true);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
