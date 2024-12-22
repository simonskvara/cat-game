using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelCompletion : MonoBehaviour
{
    private bool _canComplete;
    private bool _pressedComplete;

    private FoodManager _foodManager;

    private Menu _menuScript;

    [Header("Level Information")]
    public string levelName;
    public LevelInfo levelInfoSO;
    public string nextLevel;
    
    private void Awake()
    {
        _foodManager = GameObject.FindGameObjectWithTag("FoodManager").GetComponent<FoodManager>();
    }

    private void Start()
    {
        _menuScript = FindObjectOfType<Menu>();
        levelInfoSO.nextLevelName = nextLevel;
        Invoke(nameof(UpdateLevelInfo), 0.1f);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canComplete && !_pressedComplete)
        {
            CompleteLevel();
        }
    }

    private void UpdateLevelInfo()
    {
        levelInfoSO.levelName = levelName;
        
        levelInfoSO.collectedFood.Clear();
        
        foreach (var food in _foodManager.spawnedFood)
        {
            string fixedFoodName = food.name.Replace("(Clone)", "").Trim();

            DisplayFoodData foodData = new DisplayFoodData
            {
                foodName = fixedFoodName,
                foodSprite = food.GetComponent<SpriteRenderer>().sprite
            };
            
            levelInfoSO.collectedFood.Add(foodData);
        }
    }
    
    private void CompleteLevel()
    {
        _pressedComplete = true;
        _menuScript.LoadScene("LevelFinished");
    }
    
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door") && _foodManager.AllFoodsCollected)
        {
            _canComplete = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            _canComplete = false;
        }
    }
}
