using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public event System.Action<GameDatabase.Metadata> InitGameData;
    public event System.Action<GameMode> SetGameMode;
    public static QuizManager current;

    public enum GameMode
    {
        Unset,
        [Description("Multi Choice")] MultiChoice,
        [Description("Text Field")] TextField
    }

    [System.Serializable]
    public struct Settings
    {
        public GameMode gameMode;
        public int multiAnswerCount;
        public int numberOfRounds;
        public float timeLimit;
    }

    public Settings settings;

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
        StartCoroutine(UnsetGameMode());
    }

    IEnumerator UnsetGameMode()
    {
        yield return null;
        SetGameMode(GameMode.Unset);
    }

    public void StartGame()
    {
        WindowManager.current.HideWindow("TitleScreen");
        WindowManager.current.HideWindow("GameSettingsWindow");

        multiChoiceAnswer.answerCount = settings.multiAnswerCount;

        if (!isInit)
            InitGame();
        else
            SetGameMode(settings.gameMode);
    }

    public void Guess()
    {
        if (isAnswerSubmitted)
            return;

        switch (settings.gameMode)
        {
            case GameMode.MultiChoice:
                if (multiChoiceAnswer.GetAnswerCorrect()) { BeginCorrectAnswerSequence(); }
                else { BeginWrongAnswerSequence(); }
                break;

            case GameMode.TextField:
                if (textFieldAnswer.GetAnswerCorrect()) { BeginCorrectAnswerSequence(); }
                else { BeginWrongAnswerSequence(); }
                break;
        }
    }

    public void EndGame()
    {
        settings.gameMode = GameMode.Unset;
        SetGameMode(settings.gameMode);
        titleScreen.OpenWindow();
    }

    private void GenerateGameList()
    {
        // Generate a random list of games of a specified amount
        gameList = new List<GameDatabase.Metadata>();
        int _amount = settings.numberOfRounds;
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
        SetGameMode(settings.gameMode);

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
}
