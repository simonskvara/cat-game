using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    private Menu _menuScript;
    public Animator transition;
    public float transitionTime;

    private void Start()
    {
        GameManager.Instance.transitionAnimator = transition;
        GameManager.Instance.transitionTime = transitionTime;
        /*_menuScript = FindObjectOfType<Menu>();
        _menuScript.transitionAnimator = transition;
        _menuScript.transitionTime = transitionTime;*/
    }
}
