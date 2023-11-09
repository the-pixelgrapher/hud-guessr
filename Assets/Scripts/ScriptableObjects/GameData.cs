using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDatabase", menuName = "ScriptableObjects/GameData", order = 0)]
public class GameData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite hudGraphic;
    public string[] acceptedAnswers;
    public string[] wrongAnswers;
}
