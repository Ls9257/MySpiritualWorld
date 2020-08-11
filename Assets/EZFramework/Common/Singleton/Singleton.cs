using System;
using System.Reflection;
using UnityEngine;
using UnityScript.Core;

namespace EZFramework
{
    public class Single<T> where T : class
    {
        private static T single;

        public static T GetObj
        {
            get
            {
                if (single == null)
                {
                    ConstructorInfo[] info = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    ConstructorInfo con = Array.Find(info, c => c.GetParameters().Length == 0);
                    if (con != null)
                    {
                        single = con.Invoke(null) as T;
                    }
                    else
                    {
                        Debug.Log("仅支持私有构造函数的类");
                    }
                }
                return single;
            }
        }

    }
}