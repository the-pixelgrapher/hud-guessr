using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public event System.Action<GameData> InitGameData;
    public event System.Action<GameMode> SetGameMode;
    public static QuizManager current;

    public enum GameMode
    {
        Unset,
        MultiChoice,
        TextField
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
    [SerializeField] private GameModeData modeData;
    [SerializeField] private GameHistory history;

    [SerializeField] private MultiChoiceAnswer multiChoiceAnswer;
    [SerializeField] private TextFieldAnswer textFieldAnswer;
    [SerializeField] private TimerWidget timer;
    [SerializeField] private ProgressWidget progressWidget;
    [SerializeField] TMP_Text correctAnswerText;

    private List<GameData> gameList;
    private GameData chosenGame;
    private bool isInit;
    private bool isAnswerSubmitted;
    private bool isPlaying;
    private int roundCounter;

    private void Awake()
    {
        current = this;
        gameList = new List<GameData>();
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

        isPlaying = true;
    }

    public void InitGame()
    {
        isAnswerSubmitted = false;

        // Generate game list if empty
        if (!isInit)
        {
            GenerateGameList();
        }

        if (gameList.Count < 1)
        {
            WindowManager.current.ShowWindow("ResultsScreen");
            ResultsScreen.current.ShowResult(history.GetCorrectAnswerCount(), history.entry.Count);
            isPlaying = false;
            return;
        }

        // Select first game from list
        chosenGame = gameList[0];
        gameList.RemoveAt(0);

        // Send game data events
        InitGameData(chosenGame);
        SetGameMode(settings.gameMode);

        // Set timer widget
        if (!Mathf.Approximately(settings.timeLimit, modeData.maxTimeLimit + 1))
        {
            timer.SetTimer(settings.timeLimit);
        }
        else
        {
            timer.StopTimer();
        }

        // Set progress display
        progressWidget.SetVisibility(true);
        progressWidget.SetProgressDisplay(roundCounter, settings.numberOfRounds);

        isInit = true;
        isPlaying = true;
    }

    public bool GetIsPlaying()
    {
        return isPlaying;
    }

    public void Guess()
    {
        if (isAnswerSubmitted)
            return;

        switch (settings.gameMode)
        {
            case GameMode.MultiChoice:
                BeginAnswerSequence(multiChoiceAnswer.GetAnswerCorrect());
                break;

            case GameMode.TextField:
                BeginAnswerSequence(textFieldAnswer.GetAnswerCorrect());
                break;
        }

        timer.PauseTimer();
    }

    public void EndGame()
    {
        history.ClearHistory();
        timer.StopTimer();
        progressWidget.SetVisibility(false);
        roundCounter = 0;
        settings.gameMode = GameMode.Unset;
        SetGameMode(settings.gameMode);
        isInit = false;
        isPlaying = false;
        WindowManager.current.ShowWindow("TitleScreen");
    }

    private void GenerateGameList()
    {
        // Generate a random list of games of a specified amount
        gameList = new List<GameData>();
        int _amount = settings.numberOfRounds;
        _amount = Mathf.Clamp(_amount, 1, database.gameData.Length);

        // Add all games to temp collection
        var _tempList = new List<GameData>();
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

    private void BeginAnswerSequence(bool _correct)
    {
        if (_correct)
        {
            WindowManager.current.ShowWindow("CorrectAnswerWindow");
        }
        else
        {
            WindowManager.current.ShowWindow("IncorrectAnswerWindow");
            correctAnswerText.text = "Should've chosen " + chosenGame.displayName;
        }
        isAnswerSubmitted = true;
        history.AddEntry(chosenGame, _correct);
        roundCounter++;
        isPlaying = false;
    }
}
