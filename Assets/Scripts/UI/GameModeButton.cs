using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameModeButton : MonoBehaviour
{
    public GameModeData.Mode modeData;

    public bool startSelected;

    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private GameObject marker;
    [SerializeField] private Image icon;
    [SerializeField] private Color selectedColour;
    [SerializeField] private Color unselectedColour;

    private GameModePicker manager;

    public void Initialize(GameModeData.Mode _info, GameModePicker _manager)
    {
        modeData = _info;
        manager = _manager;

        marker.SetActive(startSelected);
        buttonText.text = modeData.displayName;
        icon.sprite = modeData.icon;
    }

    public void SelectMode()
    {
        manager.SelectGameMode(modeData.gameMode);
    }

    public void Select()
    {
        marker.SetActive(true);
        icon.color = selectedColour;
    }

    public void Deselect()
    {
        marker.SetActive(false);
        icon.color = unselectedColour;
    }
}
