using UnityEngine;
using UnityEngine.UI;

public class GameSettingsMenu : MonoBehaviour
{
    [SerializeField] private GameModePicker modePicker;
    [SerializeField] private Slider roundCountSlider;
    [SerializeField] private Slider timeLimitSlider;

    void Start()
    {
        InitializeSettingsUI();
    }

    private void InitializeSettingsUI()
    {
        modePicker.Initialize();
    }

    public void StartGameWithCurrentSettings()
    {
        QuizManager.current.StartGameMode(modePicker.GetSelectedMode());
    }
}
