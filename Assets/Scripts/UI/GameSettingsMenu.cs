using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using TMPro;


public class GameSettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown gameModeDropdown;
    [SerializeField] private Slider roundCountSlider;
    [SerializeField] private Slider timeLimitSlider;

    void Start()
    {
        InitializeSettingsUI();
    }

    private void InitializeSettingsUI()
    {
        // Add game modes to dropdown
        gameModeDropdown.ClearOptions();

        int _gameModeCount = System.Enum.GetValues(typeof(QuizManager.GameMode)).Length;
        List<string> _dropdownOptions = new List<string>();

        for (int i = 1; i < _gameModeCount; i++)
        {
            QuizManager.GameMode _mode = (QuizManager.GameMode)i;
            _dropdownOptions.Add(StringFormatter.FormatEnum(_mode));
        }
        gameModeDropdown.AddOptions(_dropdownOptions);
    }

    public void StartGameWithCurrentSettings()
    {
        QuizManager.current.StartGameMode((QuizManager.GameMode)gameModeDropdown.value + 1);
    }


    void Update()
    {
        
    }
}
