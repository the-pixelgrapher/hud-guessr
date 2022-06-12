using System.Collections.Generic;
using UnityEngine;

public class GameHistory : MonoBehaviour
{
    [System.Serializable]
    public struct HistoryEntry
    {
        public string id;
        public bool answerCorrect;
    }

    public List<HistoryEntry> entry;

    public void AddEntry(GameDatabase.Metadata _game, bool _correct)
    {
        HistoryEntry _entry = new HistoryEntry
        {
            id = _game.id,
            answerCorrect = _correct
        };

        entry.Add(_entry);
    }
}
