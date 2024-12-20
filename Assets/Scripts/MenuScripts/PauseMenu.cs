using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool GameIsPaused;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        GameIsPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        GameIsPaused = true;
    }
}
