using UnityEngine;
using DG.Tweening;

public class UITweener : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Transform rectTransform;
    [SerializeField] private TweenParams intro;

    private Vector3 initialPostion;
    private Vector3 initialRotation;

    private Tween fadeTween = null;
    private Tween scaleTween = null;
    private Tween moveTween = null;
    private Tween rotateTween = null;
    
    private void Awake()
    {
        if (rectTransform == null)
        {
            rectTransform = transform;
        }

        initialPostion = transform.localPosition;
        initialRotation = transform.localEulerAngles;
    }

    private void OnEnable()
    {
        if (intro.PlayOnEnable)
        {
            PlayIntroTween();
        }
    }

    public void PlayIntroTween()
    {
        ResetIntroTween();
        float _delay = intro.TweenInterval * transform.GetSiblingIndex() + intro.TweenDelay;

        if (canvas != null && intro.DoFade)
        {
            fadeTween = canvas.DOFade(intro.FadeEndAlpha, intro.FadeDuration)
                .SetDelay(_delay);
        }

        if (intro.DoScale)
        {
            scaleTween = rectTransform.DOScale(Vector3.one, intro.ScaleDuration)
                .SetDelay(_delay)
                .SetEase(intro.ScaleEase);
        }

        if (intro.DoMove)
        {
            moveTween = rectTransform.DOLocalMove(initialPostion, intro.MoveDuration)
                .SetDelay(_delay)
                .SetEase(intro.MoveEase);
        }

        if (intro.DoRotate)
        {
            rotateTween = rectTransform.DOLocalRotate(initialRotation, intro.RotateDuration)
                .SetDelay(_delay)
                .SetEase(intro.RotateEase);
        }
    }

    public void ResetIntroTween()
    {
        KillAllTweens();

        if (canvas != null && intro.DoFade)
        {
            canvas.alpha = 1.0f - intro.FadeEndAlpha;
        }

        if (intro.DoScale)
        {
            rectTransform.localScale = intro.ScaleStart;
        }

        if (intro.DoMove)
        {
            rectTransform.localPosition = initialPostion + intro.MoveStart;
        }

        if (intro.DoRotate)
        {
            rectTransform.localEulerAngles = initialRotation + intro.RotateStart;
        }
    }

    private void KillAllTweens()
    {
        fadeTween?.Kill(true);
        scaleTween?.Kill(true);
        moveTween?.Kill(true);
        rotateTween?.Kill(true);
    }

    private void OnDestroy()
    {
        KillAllTweens();
    }
}
