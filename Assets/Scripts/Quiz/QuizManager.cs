using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public enum GameMode
    {
        Unset,
        [Description("Multi Choice")] MultiChoice,
        [Description("Text Field")] TextField
    }

    public GameMode gameMode;
    public event System.Action<GameDatabase.Metadata> InitGameData;
    public event System.Action<GameMode> SetGameMode;
    public int numberOfRounds = 5;
    public static QuizManager current;

    [SerializeField] private GameDatabase database;
    [SerializeField] private GameHistory history;

    [SerializeField] private MultiChoiceAnswer multiChoiceAnswer;
    [SerializeField] private TextFieldAnswer textFieldAnswer;
    [SerializeField] Window titleScreen;
    [SerializeField] TMP_Text correctAnswerText;

    private List<GameDatabase.Metadata> gameList;
    private GameDatabase.Metadata chosenGame;
    private bool isInit;
    private bool isAnswerSubmitted;

    private void Awake()
    {
        current = this;
        gameList = new List<GameDatabase.Metadata>();
        StartCoroutine(InitGameMode());
    }

    IEnumerator InitGameMode()
    {
        yield return null;
        SetGameMode(gameMode);
    }

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

    public void StartGameMode(GameMode _mode)
    {
        gameMode = _mode;
        StartGame();
    }

    public void StartGame()
    {
        WindowManager.current.HideWindow("TitleScreen");
        WindowManager.current.HideWindow("GameSettingsWindow");

        if (!isInit)
            InitGame();
        else
            SetGameMode(gameMode);
    }

    public void EndGame()
    {
        gameMode = GameMode.Unset;
        SetGameMode(gameMode);
        titleScreen.OpenWindow();
    }

    private void InitGame()
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

        isInit = true;
    }

    private void BeginCorrectAnswerSequence()
    {
        WindowManager.current.ShowWindow("CorrectAnswerWindow");
        isAnswerSubmitted = true;
    }
    private void BeginWrongAnswerSequence()
    {
        WindowManager.current.ShowWindow("IncorrectAnswerWindow");
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
