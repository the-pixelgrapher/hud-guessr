using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public AnswerButton[] answerButtons;
    public GameDatabase.GameMetadata chosenGame;

    [SerializeField]
    private GameDatabase database;

    [SerializeField]
    private AnswerButton answerButtonPrefab;
    [SerializeField]
    private Transform buttonContainer;
    [SerializeField]
    private Image hudGraphicImage;

    void Start()
    {
        InitRandomGame();
        InitWrongAnswers(4);
    }

    public void InitRandomGame()
    {
        chosenGame = database.GetRandomGame();
        hudGraphicImage.sprite = chosenGame.hudGraphic;
    }

    private void InitWrongAnswers(int _amount)
    {
        
    }

}
