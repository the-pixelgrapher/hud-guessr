using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDatabase", menuName = "ScriptableObjects/GameDatabase", order = 1)]
public class GameDatabase : ScriptableObject
{
    [Serializable]
    public struct GameMetadata
    {
        public string id;
        public string displayName;
        public Sprite hudGraphic;
        public string[] wrongAnswers;
    }

    public GameMetadata[] gameData;
}
