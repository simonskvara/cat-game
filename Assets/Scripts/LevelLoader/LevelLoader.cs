using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private Menu _menuScript;
    public Animator transition;
    public float transitionTime;

    private void Start()
    {
        _menuScript = FindObjectOfType<Menu>();
        _menuScript.transitionAnimator = transition;
        _menuScript.transitionTime = transitionTime;
    }
}
