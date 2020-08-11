using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EZFramework
{
    public class WindowEditor : MonoBehaviour
    {

        #region Initialize
        [UnityEditor.MenuItem("EZFramework/Initialize", false, -11)]
        private static void Initialize()
        {
            CreateScript.Initialize();
        }
        #endregion


        #region AssetBundle
        [UnityEditor.MenuItem("EZFramework/AssetBundle", false, 0)]
        private static void BuildAssetWindows()
        {
            
            AssetBundleBuild.CreatWindow();
        }
        #endregion

        #region CreateScript
        [UnityEditor.MenuItem("EZFramework/CreateScript/DefaultScript", true, 11)]
        private static bool FolderGame()
        {
            return Selection.activeGameObject != null;
        }


        [UnityEditor.MenuItem("EZFramework/CreateScript/UGUIScript", true, 11)]
        private static bool FolderGame1()
        {
            return Selection.activeGameObject != null;
        }

        [UnityEditor.MenuItem("EZFramework/CreateScript/UGUIScript", false, 11)]
        private static void CreateFolders1()
        {
            if (FolderGame())
            {
                CreateScript.CreatFolders(Selection.activeGameObject);
            }
        }
        #endregion

        #region NameSetting
        [UnityEditor.MenuItem("EZFramework/CreateScript/NameSetting", false, 11)]
        private static void NameSetting()
        {

            ScriptNameSetting.CreatWindow();
        }
        #endregion

        //#region SaveObjMaterial
        ///// <summary>
        ///// 可以创建对象身上的材质球，用处不大
        ///// </summary>
        ///// <returns></returns>
        //[UnityEditor.MenuItem("EZFramework/SaveObjMaterial", true, 22)]
        //private static bool FolderGame2()
        //{
        //    return Selection.activeGameObject != null;
        //}

        //[UnityEditor.MenuItem("EZFramework/SaveObjMaterial", false, 22)]
        //private static void SaveObjMaterial()
        //{
        //    if (FolderGame2())
        //    {
        //        //保存材质球
        //        Material material = Selection.activeGameObject.GetComponent<MeshRenderer>().material;
        //        AssetDatabase.CreateAsset(material, $"Assets/{material}.mat");
        //        AssetDatabase.Refresh();
        //    }
        //}
        //#endregion


    }
}