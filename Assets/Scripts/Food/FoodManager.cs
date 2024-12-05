using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodManager : MonoBehaviour
{
    public FoodContainer foodContainer;
    public List<Transform> foodSpawnPoints;

    [HideInInspector] public List<GameObject> _chosenFood;

    public List<GameObject> spawnedFood;

    private int _numberOfFoodInScene;
    private int _numberOfFoodPrefabs;
    
    public bool AllFoodsCollected { get; private set; }
    

    private void Awake()
    {
        _numberOfFoodInScene = foodSpawnPoints.Count;
        _numberOfFoodPrefabs = foodContainer.foodPrefabs.Length;
    }
    
    private void Start()
    {
        if (_numberOfFoodInScene > _numberOfFoodPrefabs)
        {
            Debug.LogError("More spawn points for food, than possible unique foods\nMake sure ", gameObject);
            return;
        }
        
        //shuffle with Fisher-Yates algorithm 
        List<GameObject> shuffledFood = new List<GameObject>(foodContainer.foodPrefabs);
        for (int i = shuffledFood.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = shuffledFood[i];
            shuffledFood[i] = shuffledFood[j];
            shuffledFood[j] = temp;

        }
        
        _chosenFood = new List<GameObject>();
        for (int i = 0; i < _numberOfFoodInScene; i++)
        {
            _chosenFood.Add(shuffledFood[i % shuffledFood.Count]); // % allows for duplicates if there is more food spawnpoints than unique foods
        }

        for (int i = 0; i < _numberOfFoodInScene; i++)
        {
            GameObject food = Instantiate(_chosenFood[i], foodSpawnPoints[i].position, Quaternion.identity);
            
            spawnedFood.Add(food); 
        }
    }


    private bool notified = false;
    private void Update()
    {
        
        if (AllFoodCollectedCheck())
        {
            AllFoodsCollected = true;
            if (!notified)
            {
                Debug.Log("All food collected");
                notified = true;
            }
        }
    }

    public bool AllFoodCollectedCheck()
    {
        return spawnedFood.Count == 0;
    }
}
