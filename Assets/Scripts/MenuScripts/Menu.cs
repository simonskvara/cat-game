using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Menu : MonoBehaviour
{
    [HideInInspector] public Animator transitionAnimator;
    [HideInInspector] public float transitionTime;
    
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        StartCoroutine(Transition(sceneName));
        //SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(Transition(SceneManager.GetActiveScene().name));
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator Transition(string sceneName)
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(sceneName);
    }
    
}
