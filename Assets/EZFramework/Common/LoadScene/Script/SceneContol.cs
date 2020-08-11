using UnityEngine;
using UnityEngine.UI;
using EZFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace EZFramework
{
    public class SceneContol : MonoBehaviour
    {
        private static Stack<string> SceneNameStack = new Stack<string>();

        private const string TransferScene = "TransferScene";

        private const string SceneLoadingFace = "SceneLoadingFace";

        public static string PreloadSceneName { get; private set; }
        /// <summary>
        /// ���س���
        /// </summary>
        /// <param sceneName="��������"></param>
        /// <param UseTestScene="�Ƿ�ʹ����ת����"></param>
        public static void LoadScene(string sceneName, bool UseTestScene = true)
        {
            SceneNameStack.Push(SceneManager.GetActiveScene().name);
            Load(sceneName, UseTestScene);
        }

        /// <summary>
        /// ������һ������
        /// </summary>
        public static void LoadUpScene()
        {
            if (SceneNameStack.Count > 1)
            {
                Load(SceneNameStack.Pop());
            }

        }

        private static void Load(string sceneName, bool UseTestScene = true)
        {
            PreloadSceneName = sceneName;
            if (UseTestScene)
            {
                SceneManager.LoadScene(TransferScene);
            }
            else
            {
                Instantiate(Resources.Load("SceneLoadingFace"), GameObject.Find("Canvas").transform);
            }
        }
    }
}
