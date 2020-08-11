using EZFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadingFace : MonoBehaviour
{
    private Slider m_slider;

    private Text m_progressbar;

    private Text m_scenename;

    public void Start()
    {
        m_slider = transform.Find("Back/Slider_slider").GetComponent<Slider>();
        m_progressbar = transform.Find("Back/progressbar_text").GetComponent<Text>();
        m_scenename = transform.Find("Back/SceneName_text").GetComponent<Text>();

        AsyLoadScene(SceneContol.PreloadSceneName);
    }


    private void AsyLoadScene(string sceneName)
    {
        m_scenename.text = string.Format("正在加载场景{0}", sceneName);
        var sce = SceneManager.LoadSceneAsync(sceneName);
        sce.allowSceneActivation = false;
        while (sce.progress < 0.9f)
        {
            m_slider.value = sce.progress;
            m_progressbar.text = (int)(m_slider.value * 100) + "%";
        }
        sce.allowSceneActivation = true;
        m_slider.value = 1;
    }
}
