using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Slider slider;
    [SerializeField] private string minText;
    [SerializeField] private string maxText;

    private void Awake()
    {
        slider.onValueChanged.AddListener(val =>
        {
            SetText();
        });

        SetText();
    }

    public void SetText()
    {
        text.text = slider.value.ToString("N0");

        if (Mathf.Approximately(slider.value, slider.maxValue) && maxText != "")
        {
            text.text = maxText;
        }
        if (Mathf.Approximately(slider.value, slider.minValue) && minText != "")
        {
            text.text = minText;
        }
    }
}
