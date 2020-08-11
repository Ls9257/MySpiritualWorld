using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using UnityEditor;
using UnityEngine;
namespace EZFramework
{
    public class ScriptNameSetting : EditorWindow
    {
        private static ScriptNameSetting myWindow;
        public static void CreatWindow()
        {
            Rect rect = new Rect(0, 0, 480, 480);
            myWindow = (ScriptNameSetting)EditorWindow.GetWindowWithRect(typeof(ScriptNameSetting), rect, true, "ScriptNameSetting");//创建窗口

            scpName = ReadNameSetting();
        }

      

        private static List<string[]> scpName;
        private Vector2 scrollBarValue;
        private void OnGUI()
        {
            scrollBarValue = GUILayout.BeginScrollView(scrollBarValue, GUILayout.Width(480), GUILayout.Height(480));

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("后缀名", GUILayout.Width(210));
            GUILayout.Label("对应脚本", GUILayout.Width(210));
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < scpName.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                scpName[i][0] = EditorGUILayout.TextField(new GUIContent(), scpName[i][0]);
                scpName[i][1] = EditorGUILayout.TextField(new GUIContent(), scpName[i][1]);
                if (GUILayout.Button("删除"))
                {
                    scpName.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("添加"))
            {
                scpName.Add(new string[2]);
            }

            GUILayout.Space(40);
            if (GUILayout.Button("保存", GUILayout.Height(40)))
            {
                SaveNameSetting();
                myWindow.Close();
            }

            GUILayout.EndScrollView();
        }

        private static List<string[]> ReadNameSetting()
        {
            var value = File.ReadAllLines(GetScriptPath.GetNameSettingPath);
            List<string[]> names = new List<string[]>();
            for (int i = 0; i < value.Length; i++)
            {
                names.Add(value[i].Split(','));
            }
            return names;
        }

        private static void SaveNameSetting()
        {
            string[] newValue = new string[scpName.Count];
            for (int i = 0; i < scpName.Count; i++)
            {
                newValue[i] = string.Join(",", scpName[i]);
            }
            File.WriteAllLines(GetScriptPath.GetNameSettingPath, newValue);
        }
    }
}