using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class ResultsScreen : MonoBehaviour
{
    public static ResultsScreen current;

    [SerializeField] private TMP_Text percentageText;
    [SerializeField] private TMP_Text countText;

    private void Awake()
    {
        current = this;
    }

    public void ShowResult(int _correctAnswers, int _totalAnswers)
    {
        countText.text = _correctAnswers.ToString() + "/" + _totalAnswers.ToString();

        float percentage = (float) _correctAnswers / _totalAnswers;
        percentageText.text = percentage.ToString("P0", CultureInfo.InvariantCulture);
    }
}
