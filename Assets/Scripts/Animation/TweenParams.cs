using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class TweenParams
{
    public float FadeDuration { get { return m_fadeDuration; } }
    public float FadeEndAlpha { get { return m_fadeEndAlpha; } }
    public float ScaleDuration { get { return m_scaleDuration; } }
    public Vector3 ScaleStart { get { return m_scaleStart; } }
    public Vector3 ScaleEnd { get { return m_scaleEnd; } }
    public Ease ScaleEase { get { return m_scaleEase; } }
    public float TweenDelay { get { return m_tweenDelay; } }
    public float TweenInterval { get { return m_tweenInterval; } }

    [SerializeField] private float m_tweenDelay = 0.0f;
    [SerializeField] private float m_tweenInterval = 0.0f;

    [SerializeField, Header("Fade")] private float m_fadeDuration = 0.5f;
    [SerializeField] private float m_fadeEndAlpha = 1.0f;

    [SerializeField, Header("Scale")] private float m_scaleDuration = 0.5f;
    [SerializeField] private Vector3 m_scaleStart = Vector3.zero;
    [SerializeField] private Vector3 m_scaleEnd = Vector3.one;
    [SerializeField] private Ease m_scaleEase = Ease.OutQuad;
}
