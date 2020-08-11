using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColorSwitch : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Image SetColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color isOnColor;
    void Start()
    {

        SetColor = SetColor ?? GetComponent<Image>();
        var toggle = GetComponent<Toggle>();
        SetColor.color = toggle.isOn ? isOnColor : defaultColor;
        toggle.onValueChanged.AddListener((a) =>
        {
            SetColor.color = a ? isOnColor : defaultColor;
            if (text != null)
            {
                text.color = a ? isOnColor : defaultColor;
            }
        });

    }
    private void Reset()
    {
        defaultColor = isOnColor = Color.white;
    }
}
