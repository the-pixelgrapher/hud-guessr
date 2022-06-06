using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHistory : MonoBehaviour
{
    [System.Serializable]
    public struct History
    {
        public GameDatabase.GameMetadata game;
        public int timesShown;
    }

    public List<History> historyEntry;

    public void AddEntry(GameDatabase.GameMetadata _game)
    {
        bool _entryAdded = false;

        // Check if already in list, increment times played if so
        for (int i = 0; i < historyEntry.Count; i++)
        {
            if (historyEntry[i].game.id == _game.id)
            {
                History _tempEntry = historyEntry[i];
                _tempEntry.timesShown++;
                historyEntry[i] = _tempEntry;
                _entryAdded = true;

                Debug.Log(historyEntry[i].game.displayName + " has beem played " + historyEntry[i].timesShown + " time(s)");
            }
        }

        // Otherwise, add entry to list
        if (!_entryAdded)
        {
            History _tempEntry = new History
            {
                game = _game,
                timesShown = 0
            };

            historyEntry.Add(_tempEntry);
        }
    }
}
