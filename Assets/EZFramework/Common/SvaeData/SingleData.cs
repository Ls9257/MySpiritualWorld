using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using LitJson;
namespace EZFramework
{
    public class SingleData<T> where T : BaseData<T>
    {
        private static T data;

        public static T Data
        {
            get
            {
                if (data == null)
                {
                    data = GetData();
                    if (data == null)
                    {
                        ConstructorInfo[] info = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.Public);
                        ConstructorInfo con = Array.Find(info, c => c.GetParameters().Length == 0);
                        if (con != null)
                        {
                            data = con.Invoke(null) as T;
                        }
                        else
                        {
                            Debug.Log("数据类异常");
                        }
                    }
                }
                return data;
            }
        }

        public static T GetData()
        {
            string key = typeof(T).ToString();
            if (PlayerPrefs.HasKey(key))
            {
                string value = PlayerPrefs.GetString(key);
                return LitJson.JsonMapper.ToObject<T>(value);
            }
            return null;
        }

        public static void SaveData(T data)
        {
            string key = typeof(T).ToString();
            string value = LitJson.JsonMapper.ToJson(data);
            PlayerPrefs.SetString(key, value);
        }

    }
}
