using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public Button nextLevelButton;
    public LevelInfo levelInfo;
    public Menu menuScript;

    private void Start()
    {
        if (levelInfo.nextLevelName == "")
        {
            nextLevelButton.interactable = false;
            return;
        }
        
        nextLevelButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        menuScript.LoadScene(levelInfo.nextLevelName);
    }

    private void OnDestroy()
    {
        nextLevelButton.onClick.RemoveAllListeners();
    }
}
