using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZFramework
{
    public class CommonFunction : MonoBehaviour
    {
        /// <summary>
        /// 从索引startindex开始删除一个对象的子集
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="startindex"></param>
        public static void DestroyAllChild(Transform parent, int startindex = 0)
        {
            for (int p = startindex; p < parent.childCount; p++)
            {
                Destroy(parent.GetChild(p).gameObject);
            }
        }

        /// <summary>
        /// 获取当前的命名空间
        /// </summary>
        /// <returns></returns>
        public static string GetNameSpace(string suffix = "")
        {
            return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + suffix;
        }

        /// <summary>
        /// 获取不带命名空间的名字（限制EZFramework命名空间）
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public static string ReplaceEZ(string modelName)
        {
            return modelName.Replace(CommonFunction.GetNameSpace("."), "");
        }
    }
}
