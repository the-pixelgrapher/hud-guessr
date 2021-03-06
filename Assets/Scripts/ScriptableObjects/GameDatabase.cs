using UnityEngine;

[CreateAssetMenu(fileName = "GameDatabase", menuName = "ScriptableObjects/GameDatabase", order = 1)]
public class GameDatabase : ScriptableObject
{
    [System.Serializable]
    public struct Metadata
    {
        public string id;
        public string displayName;
        public Sprite hudGraphic;
        public string[] acceptedAnswers;
        public string[] wrongAnswers;
    }

    public Metadata[] gameData;

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

    public Metadata GetGameData(string _id)
    {
        Metadata _metaData = gameData[GetIndexFromID(_id)];
        return _metaData;
    }

    public Metadata GetRandomGame()
    {
        int _index = Random.Range(0, gameData.Length - 1);

        return gameData[_index];
    }
}