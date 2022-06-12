using UnityEngine;
using DG.Tweening;

public class UITweener : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvas;
    [SerializeField] private Transform rectTransform;
    [SerializeField] private TweenParams m_tweenSettings;

    private Tween fadeTween = null;
    private Tween scaleTween = null;

    private void Awake()
    {
        if (rectTransform == null)
        {
            rectTransform = transform;
        }
    }

    private void OnEnable()
    {
        PlayTween();
    }

    public void PlayTween()
    {
        ResetTween();

        fadeTween = m_canvas.DOFade(m_tweenSettings.FadeEndAlpha, m_tweenSettings.FadeDuration)
            .SetDelay(m_tweenSettings.TweenInterval * transform.GetSiblingIndex());

        scaleTween = rectTransform.DOScale(m_tweenSettings.ScaleEnd, m_tweenSettings.ScaleDuration)
            .SetDelay(m_tweenSettings.TweenInterval * transform.GetSiblingIndex())
            .SetEase(m_tweenSettings.ScaleEase);
    }

    public void ResetTween()
    {
        fadeTween?.Kill(true);
        scaleTween?.Kill(true);

        m_canvas.alpha = 1.0f - m_tweenSettings.FadeEndAlpha;
        rectTransform.localScale = m_tweenSettings.ScaleStart;
    }

    private void OnDestroy()
    {
        fadeTween?.Kill(true);
        scaleTween?.Kill(true);
    }
}
