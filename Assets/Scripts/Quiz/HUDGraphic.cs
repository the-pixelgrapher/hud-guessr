using UnityEngine;
using UnityEngine.UI;

public class HUDGraphic : MonoBehaviour
{
    [SerializeField] private Image hudGraphicImage;
    [SerializeField] private UITweener tweener;

    private void Start()
    {
        QuizManager.current.InitGameData += InitHUDImage;
    }

    public void InitHUDImage(GameDatabase.Metadata _gameData)
    {
        hudGraphicImage.sprite = _gameData.hudGraphic;
        tweener.PlayIntroTween();
    }
}
