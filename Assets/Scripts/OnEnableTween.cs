using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnEnableTween : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvas;
    [SerializeField] private TweenParams m_tweenSettings;

    private Tween m_fadeTween = null;
    private Tween m_scaleTween = null;

    private void OnEnable()
    {
        ResetTween();

        m_fadeTween = m_canvas.DOFade(m_tweenSettings.FadeEndAlpha, m_tweenSettings.FadeDuration)
            .SetDelay(m_tweenSettings.TweenInterval * transform.GetSiblingIndex());

        m_scaleTween = transform.DOScale(m_tweenSettings.ScaleEnd, m_tweenSettings.ScaleDuration)
            .SetDelay(m_tweenSettings.TweenInterval * transform.GetSiblingIndex())
            .SetEase(m_tweenSettings.ScaleEase);
    }

    public void ResetTween()
    {
        m_fadeTween?.Kill(true);
        m_scaleTween?.Kill(true);

        m_canvas.alpha = 1.0f - m_tweenSettings.FadeEndAlpha;
        transform.localScale = m_tweenSettings.ScaleStart;
    }
}
