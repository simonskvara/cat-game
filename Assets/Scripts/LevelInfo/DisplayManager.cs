using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    public LevelInfo levelInfo;
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private Transform parentContainer;
    [SerializeField] private TextMeshProUGUI levelNameText;
    
    
    
    private void Start()
    {
        DisplaySprites();
        DisplayLevelName();
    }

    private void DisplaySprites()
    {
        foreach (var food in levelInfo.collectedFood)
        {
            GameObject newImageObj = Instantiate(imagePrefab, parentContainer);
            Image img = newImageObj.GetComponent<Image>();
            img.sprite = food.foodSprite;
            newImageObj.GetComponentInChildren<TextMeshProUGUI>().text = food.foodName;
        }
    }

    private void DisplayLevelName()
    {
        levelNameText.text = levelInfo.levelName;
    }
    
    
}
