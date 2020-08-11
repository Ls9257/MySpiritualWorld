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
        /// 加载场景
        /// </summary>
        /// <param sceneName="场景名字"></param>
        /// <param UseTestScene="是否使用中转场景"></param>
        public static void LoadScene(string sceneName, bool UseTestScene = true)
        {
            SceneNameStack.Push(SceneManager.GetActiveScene().name);
            Load(sceneName, UseTestScene);
        }

        /// <summary>
        /// 加载上一个场景
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
