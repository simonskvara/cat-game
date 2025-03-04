using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public string textBeforeTime;
    
    private void Start()
    {
        timerText.gameObject.SetActive(Timer.Instance.showTimer);
    }

    private void Update()
    {
        if (Timer.Instance.showTimer)
        {
            timerText.text = textBeforeTime + Timer.Instance.TotalTime.ToString("0.0000");
        }
    }
}
