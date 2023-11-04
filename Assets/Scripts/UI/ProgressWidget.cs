using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ProgressWidget : MonoBehaviour
{
    [SerializeField] private GameObject widget;
    [SerializeField] private Image progressBar;
    [SerializeField] private TMP_Text progressText;

    [SerializeField] private float barTweenDuration;
    [SerializeField] private Ease barTweenEase;
    private Tween barTweener;

    public void SetProgressDisplay(int _currentRound, int _totalRounds)
    {
        _currentRound++;
        progressText.text = _currentRound.ToString() + "/" + _totalRounds.ToString();
        barTweener?.Kill(true);
        barTweener = progressBar.DOFillAmount((float)_currentRound / _totalRounds, barTweenDuration).SetEase(barTweenEase);
    }

    public void SetVisibility(bool _visible)
    {
        widget.SetActive(_visible);
    }
}
