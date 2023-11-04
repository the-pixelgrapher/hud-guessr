using UnityEngine;
using UnityEngine.UI;

public class GameSettingsMenu : MonoBehaviour
{
    [SerializeField] private GameDatabase database;
    [SerializeField] private GameModeData modeData;
    [SerializeField] private GameModePicker modePicker;
    [SerializeField] private GameObject answerCountParent;
    [SerializeField] private Slider answerCountSlider;
    [SerializeField] private Slider roundCountSlider;
    [SerializeField] private Slider timeLimitSlider;

    private void Start()
    {
        InitializeSettingsUI();
    }

    public void ApplyGameSettings()
    {
        QuizManager.current.settings.gameMode = modePicker.GetSelectedMode();
        QuizManager.current.settings.multiAnswerCount = (int)answerCountSlider.value;
        QuizManager.current.settings.numberOfRounds = (int)roundCountSlider.value;
        QuizManager.current.settings.timeLimit = (int)timeLimitSlider.value;
    }

    public void StartGameWithCurrentSettings()
    {
        ApplyGameSettings();
        QuizManager.current.StartGame();
    }

    private void Update()
    {
        // Only show answer count slider if applicable
        answerCountParent.SetActive(modePicker.GetSelectedMode() == QuizManager.GameMode.MultiChoice);
    }

    private void InitializeSettingsUI()
    {
        modePicker.Initialize();
        answerCountSlider.value = QuizManager.current.settings.multiAnswerCount;
        roundCountSlider.value = QuizManager.current.settings.numberOfRounds;
        roundCountSlider.maxValue = database.gameData.Length;
        timeLimitSlider.value = QuizManager.current.settings.timeLimit;
        timeLimitSlider.maxValue = modeData.maxTimeLimit + 1;
    }
}
