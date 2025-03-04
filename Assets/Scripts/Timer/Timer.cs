using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    public float TotalTime { get; private set; }

    private bool _canTime;
    
    public bool CanRecordTime { get; private set; }

    public TimeScores timeScores;
    
    [Header("Timer")]
    public bool showTimer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            TotalTime = 0;

            SceneManager.activeSceneChanged += SceneChanged;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_canTime)
        {
            TotalTime += Time.deltaTime;
        }
    }

    private void SceneChanged(Scene current, Scene next)
    {
        switch (next.name)
        {
            case "LevelFinished":
                _canTime = false;
                break;
            case "MainMenu":
                _canTime = false;
                CanRecordTime = false;
                TotalTime = 0;
                break;
            case "Level1":
                _canTime = true;
                CanRecordTime = true;
                break;
            default:
                break;
        }
        
    }

    public void TurnOffTimer()
    {
        _canTime = false;
    }

    public void TurnOnTimer()
    {
        _canTime = true;
    }

    public void RecordTime()
    {
        timeScores.bestTotalTime = TotalTime;
    }
}
