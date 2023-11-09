using UnityEngine;

[CreateAssetMenu(fileName = "GameDatabase", menuName = "ScriptableObjects/GameDatabase", order = 1)]
public class GameDatabase : ScriptableObject
{
    public GameData[] gameData;

    public int GetIndexFromID(string _id)
    {
        int _index = -1;

        for (int i = 0; i < gameData.Length; i++)
        {
            if (gameData[i].id == _id)
            {
                _index = i;
            }
        }

        if (_index < 0)
        {
            Debug.LogError("Could not find '" + _id + "'" );
        }

        return _index;
    }

    public GameData GetGameData(string _id)
    {
        GameData _metaData = gameData[GetIndexFromID(_id)];
        return _metaData;
    }

    public GameData GetRandomGame()
    {
        int _index = Random.Range(0, gameData.Length - 1);

        return gameData[_index];
    }
}