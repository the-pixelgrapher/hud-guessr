using UnityEngine;
using TMPro;

public class AnswerCard : MonoBehaviour
{
    public struct AnswerInfo
    {
        public string id;
        public string displayName;
    }

    public AnswerInfo answerInfo;

    [SerializeField]
    private TMP_Text buttonText;
    [SerializeField]
    private GameObject marker;

    private MultiChoiceAnswer manager;

    public void Initialize(string _id, string _displayName, MultiChoiceAnswer _manager)
    {
        answerInfo.id = _id;
        answerInfo.displayName = _displayName;
        manager = _manager;

        marker.SetActive(false);
        buttonText.text = answerInfo.displayName;
    }

    public void SelectGame()
    {
        manager.SelectAnswer(answerInfo.id);
    }

    public void Select()
    {
        marker.SetActive(true);
    }

    public void Deselect()
    {
        marker.SetActive(false);
    }
}
