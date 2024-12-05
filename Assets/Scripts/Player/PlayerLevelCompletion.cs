using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelCompletion : MonoBehaviour
{
    private bool _canComplete;
    private bool _pressedComplete;


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
        //TODO: probably just load scene to a UI saying level complete
    }
    
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
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
