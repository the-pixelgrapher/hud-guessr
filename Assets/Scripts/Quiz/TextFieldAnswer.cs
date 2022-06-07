using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFieldAnswer : AnswerBase
{
    [SerializeField] private TMP_InputField answerField;
    private string givenAnswer;

    protected override void Initialise()
    {
        base.Initialise();
        answerField.text = "";
    }


    public void GetInputFromAnswerField()
    {
        givenAnswer = answerField.text;
        guessButton.interactable = givenAnswer.Length > 1;
    }

    protected override void TestAnswerCorrect()
    {
        base.TestAnswerCorrect();

        // Add all accepted answers to list
        var _acceptedAnswers = new List<string>();

        _acceptedAnswers.Add(chosenGame.displayName);

        for (int i = 0; i < chosenGame.acceptedAnswers.Length; i++)
        {
            _acceptedAnswers.Add(chosenGame.acceptedAnswers[i]);
        }

        // Check if given answer matches any accepted answer
        for (int i = 0; i < _acceptedAnswers.Count; i++)
        {
            if (givenAnswer == _acceptedAnswers[i])
            {
                isAnswerCorrect = true;
            }
        }
    }
}
