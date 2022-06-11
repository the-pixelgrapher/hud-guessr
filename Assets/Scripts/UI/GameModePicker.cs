using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModePicker : MonoBehaviour
{
    public QuizManager.GameMode defaultMode;

    [SerializeField] private GameModeData gameModeData;
    [SerializeField] private GameModeButton gameModeButtonPrefab;
    [SerializeField] private Transform buttonParent;

    private QuizManager.GameMode selectedMode;
    private List<GameModeButton> modeButtons;

    public void Initialize()
    {
        modeButtons = new List<GameModeButton>();

        int _gameModeCount = System.Enum.GetValues(typeof(QuizManager.GameMode)).Length;

        for (int i = 1; i < _gameModeCount; i++)
        {
            GameModeButton _modeButtom = Instantiate(gameModeButtonPrefab);
            _modeButtom.transform.SetParent(buttonParent);
            _modeButtom.transform.localScale = Vector3.one;
            _modeButtom.Initialize(gameModeData.modeData[i], this);
            modeButtons.Add(_modeButtom);
        }

        SelectGameMode(defaultMode);
    }

    public QuizManager.GameMode GetSelectedMode()
    {
        return selectedMode;
    }

    public void SelectGameMode(QuizManager.GameMode _mode)
    {
        selectedMode = _mode;
        for (int i = 0; i < modeButtons.Count; i++)
        {
            if (modeButtons[i].modeData.gameMode == selectedMode)
            {
                modeButtons[i].Select();
            }
            else
            {
                modeButtons[i].Deselect();
            }
        }
    }
}
