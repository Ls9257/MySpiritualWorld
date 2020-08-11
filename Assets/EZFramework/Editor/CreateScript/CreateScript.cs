using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace EZFramework
{
    public class CreateScript : MonoBehaviour
    {
        //对象资源存放地址
        private const string Path = "/Project/";

        //Script文件夹
        private const string ScriptPath = "/Script";

        //Resources文件夹
        private const string ResourcesPath = "/Resources";

        //切图文件夹
        private const string Image = "/Image";

        /// <summary>
        /// 创建InIt脚本
        /// </summary>
        public static void Initialize()
        {
            StringBuilder sb = new StringBuilder();
            //命名空间
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using EZFramework;");
            sb.AppendLine();
            sb.AppendLine("namespace EZFramework");
            sb.AppendLine("{");
            sb.AppendLine("\tpublic class InIt : MonoBehaviour");
            sb.AppendLine("\t{");
            sb.AppendLine();
            sb.AppendLine("\t\t[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]");
            sb.AppendLine("\t\tprivate static void OnBeforeSceneLoad()");
            sb.AppendLine("\t\t{");
            sb.AppendLine();
            sb.AppendLine("\t\t\tEZModel.BaseParent = new GameObject(\"[Application]\", typeof(InIt));");
            sb.AppendLine("\t\t\tDontDestroyOnLoad(EZModel.BaseParent);");
            sb.AppendLine("\t\t\tAddModel();");
            sb.AppendLine();
            sb.AppendLine("\t\t}");
            sb.AppendLine();
            sb.AppendLine("\t\tprivate static void AddModel()");
            sb.AppendLine("\t\t{");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            System.IO.File.WriteAllText($"{Application.dataPath}/InIt.cs", sb.ToString());
            AssetDatabase.Refresh();
        }


        /// <summary>
        /// 创建脚本文件夹，预制体文件夹
        /// </summary>
        /// <param name="game"></param>
        public static void CreatFolders(GameObject game)
        {
            //创建脚本文件夹及脚本
            string scrPath = $"{Application.dataPath}{Path}{game.name}{ScriptPath}";
            CreatFolder(scrPath);
            CreateCustomUICs(game, scrPath);

            //创建预制体文件夹及预制体
            string resPath = $"{Application.dataPath}{Path}{game.name}{ResourcesPath}";
            CreatFolder(resPath);
            CreatPrefab(game, resPath);

            //创建Image文件夹
            string imgPath = $"{Application.dataPath}{Path}{game.name}{Image}";
            CreatFolder(imgPath);

        }

        /// <summary>
        /// 是否存在该路径，没有就创建
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreatFolder(string path)
        {
            DirectoryInfo mydir = new DirectoryInfo(path);
            if (!mydir.Exists)
            {
                Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
            }
            return true;
        }
        /// <summary>
        /// 自定义脚本模板
        /// </summary>
        /// <param name="csName"></param>
        /// <param name="path"></param>
        public static void CreateCustomUICs(GameObject game, string path)
        {

            StringBuilder sb = new StringBuilder();
            //命名空间
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using UnityEngine.UI;");
            sb.AppendLine("using EZFramework;");
            sb.AppendLine();

            //类开始
            sb.AppendLine($"public class {game.name} : BaseUI");
            sb.AppendLine("{");

            //生成属性
            var paths = TemplateRule(game, sb);

            //添加Start
            sb.AppendLine("\n\tpublic void Start()");
            sb.AppendLine("\t{");

            Assignment(paths, sb, game);

            sb.AppendLine();
            sb.AppendLine("\t}");

            //添加Update
            sb.AppendLine("\n\tpublic void Update()");
            sb.AppendLine("\t{");
            sb.AppendLine();
            sb.AppendLine("\t}");

            //类结尾
            sb.AppendLine();
            sb.AppendLine("}");
            System.IO.File.WriteAllText($"{path}/{game.name}.cs", sb.ToString());

            //
            //自动添加脚本(生成脚本后第一时间添加没有用)
            //game.AddComponent(Type.GetType($"{game.name}, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"));
            AssetDatabase.Refresh();

        }

        /// <summary>
        /// 从子物体上获取信息
        /// </summary>
        /// <param name="game"></param>
        /// <param name="sb"></param>
        public static List<string[]> TemplateRule(GameObject game, StringBuilder sb)
        {
            List<string[]> gamePaths = new List<string[]>();
            Transform[] childs = game.GetComponentsInChildren<Transform>();
            for (int i = 0; i < childs.Length; i++)
            {
                string[] tempname = Rules(childs[i].name.ToLower());
                if (!string.IsNullOrEmpty(tempname[0]))
                {
                    sb.AppendLine($"\n\tprivate {tempname[0]} m_{tempname[1]};");
                    gamePaths.Add(new string[] { "m_" + tempname[1], GetHierarchyPath(childs[i].gameObject, game.transform), tempname[0] });
                }
            }
            return gamePaths;
        }

        /// <summary>
        /// 获取脚本类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string[] Rules(string name)
        {
            string[] strs = name.Split('_');
            string[] values = new string[2];
            if (strs.Length < 2)
            {
                return values;
            }
            values[0] = GetScriptType(strs[strs.Length - 1]);
            strs[strs.Length - 1] = "";
            values[1] = string.Join(",", strs).Replace(",", "");
            return values;
        }
        /// <summary>
        /// 获取缩写对应的脚本名字
        /// </summary>
        private static string GetScriptType(string typename)
        {
            var value = File.ReadAllLines(GetScriptPath.GetNameSettingPath);
            List<string[]> typeNames = new List<string[]>();
            for (int i = 0; i < value.Length; i++)
            {
                typeNames.Add(value[i].Split(','));
            }
            var type = typeNames.Find(x => x[0] == typename);
            if (type != null)
            {
                return type[1];
            }
            return string.Empty;
        }

        /// <summary>
        /// 脚本生成查找对象的代码
        /// </summary>
        /// <param name="paths">paths[i][0] 属性名，paths[i][1]物体路径，paths[i][2]类名</param>
        /// <param name="sb"></param>
        /// <param name="className"></param>
        private static void Assignment(List<string[]> paths, StringBuilder sb, GameObject game)
        {
            sb.AppendLine();
            for (int i = 0; i < paths.Count; i++)
            {
                string str = string.Format("{0}{1}{2}", "\"", paths[i][1], "\"");
                if (paths[i][2] == "GameObject")//GameObject特殊处理
                {
                    sb.AppendLine($"\t\t{paths[i][0]} = transform.Find({str}).gameObject;");
                }
                else
                {
                    sb.AppendLine($"\t\t{paths[i][0]} = transform.Find({str}).GetComponent<{paths[i][2]}>();");
                }

            }
        }

        /// <summary>
        /// 获取Canvas下的路径
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static string GetHierarchyPath(GameObject gameObject, Transform tra)
        {
            List<string> paths = new List<string>();
            Transform current = gameObject.transform;
            while (current)
            {
                paths.Insert(0, current.name);
                current = current.parent == tra ? null : current.parent;
            }
            return string.Join("/", paths);
        }


        /// <summary>
        /// 创建预制体到指定路径
        /// </summary>
        /// <param name="game"></param>
        /// <param name="path"></param>
        public static void CreatPrefab(GameObject game, string path)
        {
            if (PrefabUtility.IsAnyPrefabInstanceRoot(game))
            {
                PrefabUtility.UnpackPrefabInstance(game, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            }
            path = string.Format("{0}{1}{2}{3}", path, "/", game.name, ".prefab");
            GameObject prefabObj = PrefabUtility.SaveAsPrefabAssetAndConnect(game, path, InteractionMode.AutomatedAction);
            AssetDatabase.Refresh();
        }
    }
}
