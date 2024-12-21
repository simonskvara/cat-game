using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool gameIsPaused;

    public Animator animator;

    private void Start()
    {
        pauseMenu.SetActive(false);
        animator = pauseMenu.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
        StartCoroutine(TurnOff(1f));
        animator.SetBool("GameIsPaused", false);
    }

    void Pause()
    {
        Time.timeScale = 0;
        StopAllCoroutines();
        pauseMenu.SetActive(true);
        gameIsPaused = true;
        animator.SetBool("GameIsPaused", true);
    }

    IEnumerator TurnOff(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        pauseMenu.SetActive(false);
    }
}
