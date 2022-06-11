using System.Collections.Generic;
using UnityEngine;

public class MultiChoiceAnswer : AnswerBase
{
    [Range(1, 6)] public int answerCount = 4;

    [SerializeField] private AnswerCard answerCardPrefab;
    [SerializeField] private Transform answerCardContainer;

    private string selectedAnswer = "";
    private List<AnswerCard> answerCards;
    private bool isListInit = false;

    protected override void Initialise()
    {
        base.Initialise();
        SpawnAnswerCards();
        InitAnswers();
    }

    public void SpawnAnswerCards()
    {
        if (!isListInit)
        {
            answerCards = new List<AnswerCard>();
            isListInit = true;
        }

        while (answerCards.Count < answerCount)
        {
            AnswerCard _answerCard = Instantiate(answerCardPrefab);
            _answerCard.transform.SetParent(answerCardContainer);
            _answerCard.transform.localScale = Vector3.one;
            answerCards.Add(_answerCard);
        }
    }

    public void SelectAnswer(string _id)
    {
        guessButton.interactable = true;

        selectedAnswer = _id;
        for (int i = 0; i < answerCards.Count; i++)
        {
            if (answerCards[i].answerInfo.id == selectedAnswer)
            {
                answerCards[i].Select();
            }
            else
            {
                answerCards[i].Deselect();
            }
        }
    }

    private void InitAnswers()
    {
        // Set up temporary list of answer info to randomise later
        var _answerInfoOrdered = new List<AnswerCard.AnswerInfo>();

        // Add correct answer info to list
        AnswerCard.AnswerInfo _correctAnswerInfo = new AnswerCard.AnswerInfo
        {
            id = chosenGame.id,
            displayName = chosenGame.displayName
        };
        _answerInfoOrdered.Add(_correctAnswerInfo);

        var _wrongAnswersTrimmed = new List<string>();

        // Trim slection of wrong answers to requied amount
        for (int i = 0; i < chosenGame.wrongAnswers.Length; i++)
        {
            _wrongAnswersTrimmed.Add(chosenGame.wrongAnswers[i]);
        }
        while (_wrongAnswersTrimmed.Count > answerCount - 1)
        {
            _wrongAnswersTrimmed.RemoveAt(Random.Range(0, _wrongAnswersTrimmed.Count));
        }

        // Add wrong answer info to list
        for (int i = 0; i < _wrongAnswersTrimmed.Count; i++)
        {
            AnswerCard.AnswerInfo _wongAnswerInfo = new AnswerCard.AnswerInfo
            {
                id = i.ToString(),
                displayName = _wrongAnswersTrimmed[i]
            };
            _answerInfoOrdered.Add(_wongAnswerInfo);
        }

        // Add answer into to randomised list
        var _answerInfoRandom = new List<AnswerCard.AnswerInfo>();
        while (_answerInfoOrdered.Count > 0)
        {
            int rand = Random.Range(0, _answerInfoOrdered.Count);
            _answerInfoRandom.Add(_answerInfoOrdered[rand]);
            _answerInfoOrdered.RemoveAt(rand);
        }

        // Initialise answer card with randomised answer info
        for (int i = 0; i < answerCards.Count; i++)
        {
            if (i < _answerInfoRandom.Count)
            {
                answerCards[i].gameObject.SetActive(true);
                answerCards[i].Initialize(_answerInfoRandom[i], this);
            }
            else
            {
                // If there are more cards than available answers, disable card
                answerCards[i].gameObject.SetActive(false);
            }
        }
    }

    protected override void TestAnswerCorrect()
    {
        base.TestAnswerCorrect();
        isAnswerCorrect = (selectedAnswer.Length > 0) && (selectedAnswer == chosenGame.id);
    }
}
