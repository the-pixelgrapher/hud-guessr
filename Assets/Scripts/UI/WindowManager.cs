using System;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager current;

    public event Action<string> SetWindow;
    public event Action<string> UnsetWindow;

    private void Awake()
    {
        current = this;
    }

    public void ShowWindow(string _targetWindow)
    {
        SetWindow(_targetWindow);
    }

    public void HideWindow(string _targetWindow)
    {
        UnsetWindow(_targetWindow);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
