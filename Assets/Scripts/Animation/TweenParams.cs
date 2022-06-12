using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class TweenParams
{
    // Options
    public float TweenDelay { get { return tweenDelay; } }
    public float TweenInterval { get { return tweenInterval; } }
    public bool PlayOnEnable { get { return playOnEnable; } }

    // Fade
    public bool DoFade { get { return doFade; } }
    public float FadeDuration { get { return fadeDuration; } }
    public float FadeEndAlpha { get { return fadeEndAlpha; } }

    // Scale
    public bool DoScale { get { return doScale; } }
    public float ScaleDuration { get { return scaleDuration; } }
    public Vector3 ScaleStart { get { return scaleStart; } }
    public Ease ScaleEase { get { return scaleEase; } }

    // Scale
    public bool DoMove { get { return doMove; } }
    public float MoveDuration { get { return moveDuration; } }
    public Vector3 MoveStart { get { return moveStart; } }
    public Ease MoveEase { get { return moveEase; } }

    // Rotate
    public bool DoRotate { get { return doRotate; } }
    public float RotateDuration { get { return rotateDuration; } }
    public Vector3 RotateStart { get { return rotateStart; } }
    public Ease RotateEase { get { return rotateEase; } }

    [SerializeField] private float tweenDelay = 0.0f;
    [SerializeField] private float tweenInterval = 0.0f;
    [SerializeField] private bool playOnEnable = true;

    [SerializeField, Header("Fade")] private bool doFade = false;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float fadeEndAlpha = 1.0f;

    [SerializeField, Header("Scale")] private bool doScale = false;
    [SerializeField] private float scaleDuration = 0.5f;
    [SerializeField] private Vector3 scaleStart = Vector3.zero;
    [SerializeField] private Ease scaleEase = Ease.OutQuad;

    [SerializeField, Header("Move")] private bool doMove = false;
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private Vector3 moveStart = Vector3.zero;
    [SerializeField] private Ease moveEase = Ease.OutQuad;

    [SerializeField, Header("Rotate")] private bool doRotate = false;
    [SerializeField] private float rotateDuration = 0.5f;
    [SerializeField] private Vector3 rotateStart = Vector3.zero;
    [SerializeField] private Ease rotateEase = Ease.OutQuad;
}
