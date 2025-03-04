using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerToggle : MonoBehaviour
{
    public Toggle timerToggle;

    private void Awake()
    {
        if (GetToggleState("ShowTimer") == "Yes")
        {
            timerToggle.isOn = true;
        }
        else
        {
            timerToggle.isOn = false;
        }
    }

    private void Start()
    {
        Timer.Instance.showTimer = timerToggle.isOn;
    }

    public void ChangeShowTimer()
    {
        Timer.Instance.showTimer = timerToggle.isOn;
        if (timerToggle.isOn)
        {
            PlayerPrefs.SetString("ShowTimer", "Yes");
        }
        else
        {
            PlayerPrefs.SetString("ShowTimer", "No");
        }
    }
    
    private string GetToggleState(string keyName)
    {
        return PlayerPrefs.GetString(keyName);
    }
}
