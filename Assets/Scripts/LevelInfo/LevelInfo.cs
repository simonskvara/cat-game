using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Level Info")]
public class LevelInfo : ScriptableObject
{
    public string levelName;
    public string nextLevelName;
    public List<DisplayFoodData> collectedFood;
}

[Serializable]
public struct DisplayFoodData
{
    public string foodName;
    public Sprite foodSprite;
}