using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodManager : MonoBehaviour
{
    public FoodContainer foodContainer;
    public List<Transform> foodSpawnPoints;

    [SerializeField] private List<GameObject> _chosenFood;

    private int _numberOfFoodInScene;
    private int _numberOfFoodPrefabs;
    

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
            _chosenFood.Add(shuffledFood[i % shuffledFood.Count]);
        }

        for (int i = 0; i < _numberOfFoodInScene; i++)
        {
            Instantiate(_chosenFood[i], foodSpawnPoints[i].position, Quaternion.identity);
        }
    }
}
