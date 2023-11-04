using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressWidget : MonoBehaviour
{
    [SerializeField] private GameObject widget;
    [SerializeField] private Image progressBar;
    [SerializeField] private TMP_Text progressText;

    public void SetProgressDisplay(int _currentRound, int _totalRounds)
    {
        _currentRound++;
        progressText.text = _currentRound.ToString() + "/" + _totalRounds.ToString();
        progressBar.fillAmount = (float) _currentRound / _totalRounds;
    }

    public void SetVisibility(bool _visible)
    {
        widget.SetActive(_visible);
    }
}
