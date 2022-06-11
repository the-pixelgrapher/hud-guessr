using UnityEngine;
using DG.Tweening;

public partial class Window : MonoBehaviour
{
    public string windowID;

    [SerializeField] private bool startShown = false;
    [SerializeField] private CanvasGroup blanketCanvas = null;
    [SerializeField] private CanvasGroup windowCanvas = null;

    [SerializeField] private float blanketFadeTime = 0.25f;
    [SerializeField] private float windowFadeTime = 0.175f;
    [SerializeField] private float windowScaleTime = 0.35f;
    [SerializeField] private Vector3 windowStartScale = Vector3.one;
    [SerializeField] private Ease windowScaleEase = Ease.OutBack;

    private Tween blanketTween = null;
    private Tween windowFadeTween = null;
    private Tween windowScaleTween = null;

    private bool isShowing = false;

    private void Start()
    {
        WindowManager.current.SetWindow += OnShowWindow;
        WindowManager.current.UnsetWindow += OnHideWindow;

        isShowing = startShown;
        blanketCanvas.gameObject.SetActive(isShowing);
        blanketCanvas.alpha = isShowing ? 1.0f : 0.0f;
        windowCanvas.transform.localScale = Vector3.one;
        windowCanvas.gameObject.SetActive(isShowing);
        windowCanvas.alpha = isShowing ? 1.0f : 0.0f;
    }

    public void OpenWindow()
    {
        KillAllTweens();

        if (blanketCanvas != null)
        {
            blanketCanvas.gameObject.SetActive(true);
            blanketTween = blanketCanvas.DOFade(1.0f, blanketFadeTime);
        }

        if (windowCanvas != null)
        {
            windowCanvas.gameObject.SetActive(true);
            windowFadeTween = windowCanvas.DOFade(1.0f, windowFadeTime);
            windowCanvas.transform.localScale = windowStartScale;
            windowScaleTween = windowCanvas.transform.DOScale(Vector3.one, windowScaleTime).SetEase(windowScaleEase);

        }

        isShowing = true;
    }

    public void CloseWindow()
    {
        KillAllTweens();

        if (blanketCanvas != null)
        {
            blanketTween = blanketCanvas.DOFade(0.0f, blanketFadeTime)
                .OnComplete(() => 
                    blanketCanvas.gameObject.SetActive(false)
                );
        }

        if (windowCanvas != null)
        {
            windowFadeTween = windowCanvas.DOFade(0.0f, windowFadeTime)
                .OnComplete(() => 
                    windowCanvas.gameObject.SetActive(false)
                );

            windowScaleTween = windowCanvas.transform.DOScale(windowStartScale, windowScaleTime);
        }

        isShowing = false;
    }

    public void ToggleWindow()
    {
        if (isShowing)
        {
            CloseWindow();
        }
        else
        {
            OpenWindow();
        }
    }

    public void SetWindowInteraction(bool _interactable)
    {
        if (windowCanvas != null)
        {
            windowCanvas.interactable = _interactable;
        }
    }

    private void OnShowWindow(string _targetWindow)
    {
        if (_targetWindow == windowID && !isShowing)
        {
            OpenWindow();
        }
    }

    private void OnHideWindow(string _targetWindow)
    {
        if (_targetWindow == windowID && isShowing)
        {
            CloseWindow();
        }
    }

    private void KillAllTweens()
    {
        blanketTween?.Kill(true);
        KillWindowTweens();
    }

    private void KillWindowTweens()
    {
        windowFadeTween?.Kill(true);
        windowScaleTween?.Kill(true);
    }

    private void OnDestroy()
    {
        WindowManager.current.SetWindow -= OnShowWindow;
        WindowManager.current.UnsetWindow -= OnShowWindow;
    }
}

public partial class Window
{
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (windowID == "")
        {
            windowID = gameObject.name;
        }
    }
#endif
}
