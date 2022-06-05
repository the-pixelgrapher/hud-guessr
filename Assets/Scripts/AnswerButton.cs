using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    public string id;

    [SerializeField]
    private TMP_Text buttonText;
    [SerializeField]
    private GameObject marker;

    private QuizManager manager;

    public void Initialize(string _id, string _displayName, QuizManager _manager)
    {
        marker.SetActive(false);
        id = _id;
        buttonText.text = _displayName;
        manager = _manager;
    }

    public void SelectGame()
    {
        manager.SelectAnswer(id);
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
