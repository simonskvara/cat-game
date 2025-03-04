using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerLevelCompletion : MonoBehaviour
{
    private bool _canComplete;
    private bool _pressedComplete;

    private FoodManager _foodManager;

    [Header("Level Information")]
    public LevelInfo levelInfoSO;
    public string levelName;
    [Tooltip("Leave empty if there is no next level")]
    public string nextLevel;
    public int levelIndex;

    private PlayerControls _playerControls;
    private InputAction _interact;

    [Header("TimeInfo")] public TimeScores timeScores;
    
    private void Awake()
    {
        _foodManager = GameObject.FindGameObjectWithTag("FoodManager").GetComponent<FoodManager>();
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _interact = _playerControls.Player.Interact;
        _interact.Enable();
        _interact.performed += CompleteLevel;
    }

    private void OnDisable()
    {
        _interact.Disable();
    }

    private void Start()
    {
        levelInfoSO.nextLevelName = nextLevel;
        Invoke(nameof(UpdateLevelInfo), 0.1f);
    }

    private void UpdateLevelInfo()
    {
        levelInfoSO.levelName = levelName;
        
        levelInfoSO.collectedFood.Clear();
        
        foreach (var food in _foodManager.spawnedFood)
        {
            string spawnedFoodName = food.GetComponent<FoodItem>().foodName;

            DisplayFoodData foodData = new DisplayFoodData
            {
                foodName = spawnedFoodName,
                foodSprite = food.GetComponent<SpriteRenderer>().sprite
            };
            
            levelInfoSO.collectedFood.Add(foodData);
        }
    }
    
    private void CompleteLevel(InputAction.CallbackContext context)
    {
        if (_canComplete && !_pressedComplete)
        {
            _pressedComplete = true;
        
            PlayerPrefs.SetInt($"Level_{levelIndex}_Completed", 1);
            PlayerPrefs.Save();
            
            Timer.Instance.TurnOffTimer();

            if (nextLevel == "")
            {
                RecordTime();
            }
            
            GameManager.Instance.LevelLoader("LevelFinished");
        }
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

    private void RecordTime()
    {
        float currentBestTime = Timer.Instance.timeScores.bestTotalTime;

        if (currentBestTime == 0)
        {
            Timer.Instance.RecordTime();
        }
        
        if (Timer.Instance.TotalTime < currentBestTime && Timer.Instance.TotalTime != 0)
        {
            Timer.Instance.RecordTime();
        }
    }
}
