using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using System.Collections;
using System;
using UnityEngine.Events;
using System.Runtime.CompilerServices;

public class ChooseConfirm : BaseUI
{

    private Button m_defaultaffirm;

    private Button m_close;

    private Text m_hint;

    private string hint;

    private UnityAction callBack;

    public override void Bengin<T>(T value)
    {
        Hashtable hashtable = value as Hashtable;
        hint = hashtable["value"] as string;
        callBack = hashtable["callBack"] as UnityAction;
    }

    public void Start()
    {

        m_defaultaffirm = transform.Find("Panel/DefaultAffirm_btn").GetComponent<Button>();
        m_close = transform.Find("Panel/Close_btn").GetComponent<Button>();
        m_hint = transform.Find("Panel/MainHint_text").GetComponent<Text>();
        m_hint.text = hint;

        m_close.onClick.AddListener(() =>
        {
            UIModel.CloseUIPanel<ChooseConfirm>();
        });
        m_defaultaffirm.onClick.AddListener(() =>
        {
            UIModel.CloseUIPanel<ChooseConfirm>();
            callBack.Invoke();
        });


    }

    public void Update()
    {

    }

}
