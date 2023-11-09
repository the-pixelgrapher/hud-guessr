using UnityEngine;
using UnityEngine.UI;

public abstract class AnswerBase : MonoBehaviour
{
    [SerializeField] protected QuizManager.GameMode gameMode;
    [SerializeField] protected Button guessButton;

    protected GameData chosenGame;
    protected bool isAnswerCorrect;

    public bool GetAnswerCorrect()
    {
        TestAnswerCorrect();
        return isAnswerCorrect;
    }

    private void Start()
    {
        guessButton.interactable = false;
        QuizManager.current.InitGameData += InitGameData;
        QuizManager.current.SetGameMode += ApplyGameMode;
    }

    protected void ApplyGameMode(QuizManager.GameMode _mode)
    {
        gameObject.SetActive(_mode == gameMode);
    }

    protected virtual void Initialise()
    {
    }

    protected virtual void TestAnswerCorrect()
    {
        isAnswerCorrect = false;
    }

    private void InitGameData(GameData _gameData)
    {
        chosenGame = _gameData;
        guessButton.interactable = false;
        Initialise();
    }

    private void OnDestroy()
    {
        QuizManager.current.InitGameData -= InitGameData;
        QuizManager.current.SetGameMode -= ApplyGameMode;
    }
}
