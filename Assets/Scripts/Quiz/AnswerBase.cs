using UnityEngine;
using UnityEngine.UI;

public abstract class AnswerBase : MonoBehaviour
{
    [SerializeField] protected Button guessButton;

    protected GameDatabase.Metadata chosenGame;
    protected bool isAnswerCorrect;

    private void Start()
    {
        guessButton.interactable = false;
        QuizManager.current.initGameData += InitGameData;
    }

    private void InitGameData(GameDatabase.Metadata _gameData)
    {
        chosenGame = _gameData;
        guessButton.interactable = false;
        Initialise();
    }

    protected virtual void Initialise()
    {
    }

    protected virtual void TestAnswerCorrect()
    {
    }

    public bool GetAnswerCorrect()
    {
        TestAnswerCorrect();
        return isAnswerCorrect;
    }
}
