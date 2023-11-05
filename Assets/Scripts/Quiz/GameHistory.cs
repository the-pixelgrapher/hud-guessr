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

    public int GetCorrectAnswerCount()
    {
        int count = 0;

        for (int i = 0; i < entry.Count; i++)
        {
            if (entry[i].answerCorrect)
            {
                count++;
            }
        }

        return count;
    }

    public void ClearHistory()
    {
        entry.Clear();
    }
}
