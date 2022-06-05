using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    public string id;

    [SerializeField]
    private TMP_Text buttonText;

    public void Initialize(string _id, string _displayName)
    {
        id = _id;
        buttonText.text = _displayName;
    }

    public void Select()
    {

    }

}
