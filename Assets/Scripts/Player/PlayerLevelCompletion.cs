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
    
    private void Awake()
    {
        _foodManager = GameObject.FindGameObjectWithTag("FoodManager").GetComponent<FoodManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canComplete && !_pressedComplete)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        _pressedComplete = true;
        SceneManager.LoadScene("LevelFinished");
        //TODO: make it so that only one scene is needed, and just load info in there, probably PlayerPrefs?
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
