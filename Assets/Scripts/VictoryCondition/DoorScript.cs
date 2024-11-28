using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private FoodManager _foodManager;

    public Animator doorAnimator;
    public Animator bubbleAnimator;

    private void Awake()
    {
        _foodManager = GameObject.FindGameObjectWithTag("FoodManager").GetComponent<FoodManager>();
    }

    private void Update()
    {
        if (_foodManager.AllFoodsCollected)
        {
            doorAnimator.SetBool("OpenDoor", true);
        }
    }
}
