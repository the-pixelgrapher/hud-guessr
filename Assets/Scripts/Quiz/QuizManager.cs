using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public enum GameMode
    {
        MultiChoice,
        TextField
    }

    public GameMode gameMode;
    public int numberOfRounds = 5;

    [SerializeField] private GameDatabase database;
    [SerializeField] private GameHistory history;

    [SerializeField]  private HUDGraphic[] hudGraphics;
    [SerializeField] private MultiChoiceAnswer multiChoiceAnswer;
    [SerializeField] Window correctAnswerWindow;
    [SerializeField] Window incorrectAnswerWindow;
    [SerializeField] TMP_Text correctAnswerText;

    private List<GameDatabase.GameMetadata> gameList;
    private GameDatabase.GameMetadata chosenGame;

    void Start()
    {
        gameList = new List<GameDatabase.GameMetadata>();
    }

    private void GenerateGameList()
    {
        // Generate a random list of games of a specified amount
        gameList = new List<GameDatabase.GameMetadata>();
        int _amount = numberOfRounds;
        _amount = Mathf.Clamp(_amount, 1, database.gameData.Length);

        // Add all games to temp collection
        var _tempList = new List<GameDatabase.GameMetadata>();
        for (int i = 0; i < database.gameData.Length; i++)
        {
            _tempList.Add(database.gameData[i]);
        }

        // Add random game from temp collection to main list
        while (gameList.Count < _amount)
        {
            int rand = Random.Range(0, _tempList.Count);
            gameList.Add(_tempList[rand]);
            _tempList.RemoveAt(rand);
        }
    }

    public void InitGame()
    {
        // Generate game list if empty
        if (gameList.Count < 1)
        {
            GenerateGameList();
        }
        // Select first game from list
        chosenGame = gameList[0];
        gameList.RemoveAt(0);

        history.AddEntry(chosenGame);

        foreach (HUDGraphic _graphic in hudGraphics)
        {
            _graphic.InitHUDImage(chosenGame);
        }

        switch (gameMode)
        {
            case GameMode.MultiChoice:
                multiChoiceAnswer.InitGameData(chosenGame);
                break;

            case GameMode.TextField:
                break;
        }
    }

    private void BeginCorrectAnswerSequence()
    {
        correctAnswerWindow.OpenWindow();
    }
    private void BeginWrongAnswerSequence()
    {
        incorrectAnswerWindow.OpenWindow();
        correctAnswerText.text = "The game was: " + chosenGame.displayName;
    }

    public void Guess()
    {
        switch (gameMode)
        {
            case GameMode.MultiChoice:
                if (multiChoiceAnswer.TestAnswer()) { BeginCorrectAnswerSequence(); }
                else{ BeginWrongAnswerSequence(); }
                break;

            case GameMode.TextField:
                break;
        }
    }
}
