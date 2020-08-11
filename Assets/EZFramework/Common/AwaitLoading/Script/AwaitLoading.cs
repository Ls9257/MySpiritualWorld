using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using System.Security.Permissions;

public class AwaitLoading : BaseUI
{

    private Transform m_loading;

    private Text m_hint;

    private string hint;

    public override void Bengin<T>(T value)
    {
        hint = value as string;

        if (string.IsNullOrEmpty(hint))
        {
            hint = "µ»¥˝º”‘ÿ÷–...";
        }

    }

    public void Start()
    {

        m_loading = transform.Find("Loading_tra").GetComponent<Transform>();
        m_hint = transform.Find("Hint_text").GetComponent<Text>();

        m_hint.text = hint;
    }

    public void Update()
    {
        m_loading.Rotate(-Vector3.forward, 1);
    }



}
