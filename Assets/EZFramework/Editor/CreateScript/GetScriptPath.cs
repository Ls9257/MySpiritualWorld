using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EZFramework
{
    public class GetScriptPath : MonoBehaviour
    {


        public static string GetNameSettingPath
        {
            get => string.Format($"{GetPath("ScriptNameSetting")}/NameData.txt");
        }

        public static string GetPath(string _scriptName)
        {
            string[] path = UnityEditor.AssetDatabase.FindAssets(_scriptName);
            if (path.Length > 1)
            {
                Debug.LogError("有同名文件" + _scriptName + "获取路径失败");
                return null;
            }
            //将字符串中得脚本名字和后缀统统去除掉
            string _path = AssetDatabase.GUIDToAssetPath(path[0]).Replace((@"/" + _scriptName + ".cs"), "");
            return _path;
        }

    }
}
