using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerWidget : MonoBehaviour
{
    public float timeLeft;

    [SerializeField] private GameObject widget;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Image timerRing;

    private bool timerStarted;
    private float startTime;
    private bool isPaused;

    public void SetTimer(float _time)
    {
        timeLeft = _time;
        startTime = _time;
        timerStarted = true;
        isPaused = false;
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void StopTimer()
    {
        timerStarted = false;
        isPaused = false;
    }

    private void Update()
    {
        // Subtract timer
        if (timerStarted && QuizManager.current.GetIsPlaying() && !isPaused)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0.0f)
            {
                QuizManager.current.Guess();
                timerStarted = false;
            }
        }

        widget.SetActive(timerStarted);

        // Update widget
        float _percent = timeLeft / startTime;
        timerText.text = Mathf.Ceil(timeLeft).ToString();
        timerRing.fillAmount = _percent;
    }
}
