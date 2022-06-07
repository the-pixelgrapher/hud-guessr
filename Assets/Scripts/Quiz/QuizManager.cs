using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public enum GameMode
    {
        Unset,
        MultiChoice,
        TextField
    }

    public GameMode gameMode;
    public int numberOfRounds = 5;
    public static QuizManager current;

    [SerializeField] private GameDatabase database;
    [SerializeField] private GameHistory history;

    [SerializeField] private MultiChoiceAnswer multiChoiceAnswer;
    [SerializeField] private TextFieldAnswer textFieldAnswer;
    [SerializeField] Window correctAnswerWindow;
    [SerializeField] Window incorrectAnswerWindow;
    [SerializeField] TMP_Text correctAnswerText;

    private List<GameDatabase.Metadata> gameList;
    private GameDatabase.Metadata chosenGame;
    private bool isAnswerSubmitted;

    private void Awake()
    {
        current = this;
        gameList = new List<GameDatabase.Metadata>();
    }

    public event System.Action<GameDatabase.Metadata> InitGameData;
    public event System.Action<GameMode> SetGameMode;

    private void GenerateGameList()
    {
        // Generate a random list of games of a specified amount
        gameList = new List<GameDatabase.Metadata>();
        int _amount = numberOfRounds;
        _amount = Mathf.Clamp(_amount, 1, database.gameData.Length);

        // Add all games to temp collection
        var _tempList = new List<GameDatabase.Metadata>();
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
        isAnswerSubmitted = false;

        // Generate game list if empty
        if (gameList.Count < 1)
        {
            GenerateGameList();
        }
        // Select first game from list
        chosenGame = gameList[0];
        gameList.RemoveAt(0);

        history.AddEntry(chosenGame);

        InitGameData(chosenGame);
        SetGameMode(gameMode);
    }

    private void BeginCorrectAnswerSequence()
    {
        correctAnswerWindow.OpenWindow();
        isAnswerSubmitted = true;
    }
    private void BeginWrongAnswerSequence()
    {
        incorrectAnswerWindow.OpenWindow();
        correctAnswerText.text = "The game was: " + chosenGame.displayName;
        isAnswerSubmitted = true;
    }

    public void Guess()
    {
        if (isAnswerSubmitted)
            return;

        switch (gameMode)
        {
            case GameMode.MultiChoice:
                if (multiChoiceAnswer.GetAnswerCorrect()) { BeginCorrectAnswerSequence(); }
                else{ BeginWrongAnswerSequence(); }
                break;

            case GameMode.TextField:
                if (textFieldAnswer.GetAnswerCorrect()) { BeginCorrectAnswerSequence(); }
                else { BeginWrongAnswerSequence(); }
                break;
        }
    }
}
