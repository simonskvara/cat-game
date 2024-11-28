using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    private FoodManager _foodManager;

    private void Start()
    {
        _foodManager = GameObject.FindGameObjectWithTag("FoodManager").GetComponent<FoodManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _foodManager.spawnedFood.Remove(gameObject);
        Destroy(gameObject);
    }
}
