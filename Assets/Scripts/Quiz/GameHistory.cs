using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHistory : MonoBehaviour
{
    public List<string> historyEntry;

    public void AddEntry(GameDatabase.Metadata _game)
    {
        historyEntry.Add(_game.id);
    }
}
