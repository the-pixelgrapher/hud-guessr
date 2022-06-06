using UnityEngine;
using UnityEngine.UI;

public class HUDGraphic : MonoBehaviour
{
    [SerializeField] private Image hudGraphicImage;

    public void InitHUDImage(GameDatabase.Metadata _gameData)
    {
        hudGraphicImage.sprite = _gameData.hudGraphic;
        Debug.Log("Loaded HUD graphic for: " + _gameData.id);
    }
}
