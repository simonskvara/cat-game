using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Menu : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameManager.Instance.LevelLoader(sceneName);
    }

    public void ReloadScene()
    {
        GameManager.Instance.LevelLoader(SceneManager.GetActiveScene().name);
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
