using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<AnswerButton> answerButtons;
    public GameDatabase.GameMetadata chosenGame;
    public string selectedAnswer;

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
        InitAnswers(4);
    }

    public void InitRandomGame()
    {
        chosenGame = database.GetRandomGame();
        hudGraphicImage.sprite = chosenGame.hudGraphic;
    }

    private void InitAnswers(int _amount)
    {
        // Add correct answer to container
        AnswerButton _correctAnswerButton = Instantiate(answerButtonPrefab);
        _correctAnswerButton.transform.SetParent(buttonContainer);
        _correctAnswerButton.transform.localScale = Vector3.one;
        _correctAnswerButton.Initialize(chosenGame.id, chosenGame.displayName, this);
        answerButtons.Add(_correctAnswerButton);

        var _wrongAnswersTrimmed = new List<string>();

        // Trim slection of wrong answers to requied amount
        for (int i = 0; i < chosenGame.wrongAnswers.Length; i++)
        {
            _wrongAnswersTrimmed.Add(chosenGame.wrongAnswers[i]);
        }
        while (_wrongAnswersTrimmed.Count > _amount - 1)
        {
            _wrongAnswersTrimmed.RemoveAt(Random.Range(0, _wrongAnswersTrimmed.Count - 1));
        }

        // Add all possible answers to ordered list

        for (int i = 0; i < _wrongAnswersTrimmed.Count; i++)
        {
            AnswerButton _answerButton = Instantiate(answerButtonPrefab);
            _answerButton.transform.SetParent(buttonContainer);
            _answerButton.transform.localScale = Vector3.one;
            _answerButton.Initialize(i.ToString(), _wrongAnswersTrimmed[i], this);
            answerButtons.Add(_answerButton);
        }
    }

    public void SelectAnswer(string _id)
    {
        selectedAnswer = _id;
        for (int i = 0; i < answerButtons.Count; i++)
        {
            if (answerButtons[i].id == selectedAnswer)
            {
                answerButtons[i].Select();
            }
            else
            {
                answerButtons[i].Deselect();
            }
        }
    }

    public void TestAnswer ()
    {
        if (selectedAnswer == chosenGame.id)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect. Correct answer was: " + chosenGame.displayName);
        }
    }
}
