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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _foodManager.AllFoodsCollected)
        {
            bubbleAnimator.SetBool("BubbleOpen", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _foodManager.AllFoodsCollected)
        {
            bubbleAnimator.SetBool("BubbleOpen", false);
        }
    }
}
