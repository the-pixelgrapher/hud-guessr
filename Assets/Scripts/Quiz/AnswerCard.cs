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

    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private GameObject marker;
    [SerializeField] private UITweener tweener;

    private MultiChoiceAnswer manager;

    public void Initialize(AnswerInfo _info, MultiChoiceAnswer _manager)
    {
        answerInfo = _info;
        manager = _manager;

        marker.SetActive(false);
        buttonText.text = answerInfo.displayName;
        tweener.PlayTween();
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
