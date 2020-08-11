using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using System;
using System.Collections;

public class ConfirmHint : BaseUI
{
    private Button m_defaultaffirm;

    private Text m_hint;

    private string hint;

    public override void Bengin<T>(T value)
    {
        if (!string.IsNullOrEmpty(value as string))
        {
            hint = value as string;
        }
    }

    public void Start()
    {
        m_defaultaffirm = transform.Find("Panel/DefaultAffirm_btn").GetComponent<Button>();
        m_hint = transform.Find("Panel/MainHint_text").GetComponent<Text>();
        m_hint.text = hint;

        m_defaultaffirm.onClick.AddListener(() => 
        {
            UIModel.CloseUIPanel<ConfirmHint>();
        });
    }



}
