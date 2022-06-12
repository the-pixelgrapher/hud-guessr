using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerWidget : MonoBehaviour
{
    public float timeLeft;

    [SerializeField] private GameObject widget;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Image timerRing;
    [SerializeField] private UITweener tweener;
    [SerializeField, Tooltip("When timer reaches this value, a pulse tween will play every second")] 
    private int criticalTimeThreshold = 3;
    private int prevTimeValue;

    private bool timerStarted;
    private float startTime;
    private bool isPaused;

    public void SetTimer(float _time)
    {
        timeLeft = _time;
        startTime = _time;
        timerStarted = true;
        isPaused = false;
        tweener.PlayIntroTween();
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
        int _displayTime = Mathf.CeilToInt(timeLeft);
        timerText.text = _displayTime.ToString();
        timerRing.fillAmount = _percent;

        // Play pulse animation every second when at or below criticalTimeThreshold
        if (_displayTime <= criticalTimeThreshold && _displayTime < prevTimeValue)
        {
            tweener.PlayIntroTween();
        }
        prevTimeValue = _displayTime;
    }
}
