using UnityEngine;
using TMPro;

public class TextFieldAnswer : AnswerBase
{
    [SerializeField] private TMP_InputField answerField;
    private string givenAnswer;

    protected override void Initialise()
    {
        base.Initialise();
    }


    public void GetInputFromAnswerField()
    {
        givenAnswer = answerField.text;
        guessButton.interactable = givenAnswer.Length > 1;
    }

    protected override void TestAnswerCorrect()
    {
        base.TestAnswerCorrect();
    }
}
