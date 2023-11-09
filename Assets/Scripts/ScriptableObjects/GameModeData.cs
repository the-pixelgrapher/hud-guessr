using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModesData", menuName = "ScriptableObjects/GameModeData", order = 2)]
public class GameModeData : ScriptableObject
{
    [System.Serializable]
    public struct Mode
    {
        public string displayName;
        public string description;
        public QuizManager.GameMode gameMode;
        public Sprite icon;
    }

    public Mode[] modeData;

    public int maxTimeLimit;
}
