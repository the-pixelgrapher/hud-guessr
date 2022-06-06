using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHistory : MonoBehaviour
{
    public struct History
    {
        public GameDatabase.GameMetadata gameData;
        public int timesShown;
    }

    public List<History> historyEntry;
}
