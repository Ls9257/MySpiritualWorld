using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZFramework
{
    public class EZModel : MonoBehaviour
    {
        public static GameObject BaseParent;

        private static Dictionary<string, BaseModel> AllModel = new Dictionary<string, BaseModel>();

        public static void AddModel<T>() where T : BaseModel
        {
            string modelName = typeof(T).ToString();
            modelName = CommonFunction.ReplaceEZ(modelName);
            if (!AllModel.ContainsKey(modelName))
            {
                T model = Instantiate(Resources.Load<T>(modelName), BaseParent.transform, false);
                model.name = modelName;
                model.Begin();
                AllModel.Add(modelName, model);
            }
        }

        public static T GetModel<T>() where T : BaseModel
        {
            string modelName = typeof(T).ToString();
            modelName = CommonFunction.ReplaceEZ(modelName);
            if (AllModel.ContainsKey(modelName))
            {
                return (T)AllModel[modelName];
            }
            return default;
        }

        public static void RemoveModel<T>() where T : BaseModel
        {
            string modelName = typeof(T).ToString();
            modelName = CommonFunction.ReplaceEZ(modelName);
            if (AllModel.ContainsKey(modelName))
            {
                AllModel[modelName].End();
                AllModel.Remove(modelName);
            }
        }


    }
}
