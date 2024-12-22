using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            bool isUnlocked = PlayerPrefs.GetInt($"Level_{levelIndex}_Completed", 0) == 1;
            
            levelButtons[i].interactable = isUnlocked || levelIndex == 1;
        }
    }
}
