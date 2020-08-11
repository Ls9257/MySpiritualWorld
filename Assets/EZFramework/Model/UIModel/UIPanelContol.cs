using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EZFramework
{
    public partial class UIModel : BaseModel
    {
        private static Dictionary<string, BaseUI> AllPanel = new Dictionary<string, BaseUI>();

        #region 通用方法
        public static BaseUI ShowUIPanel<T>(PanelHierarchy panel = default)
        {
            return CreatPanel(typeof(T).ToString(), panel);
        }

        public static BaseUI ShowUIPanel(string panelName, PanelHierarchy panel = default)
        {
            return CreatPanel(panelName, panel);
        }

        public static BaseUI ShowUIPanel<T>(string panelName, T value, PanelHierarchy panel = default)
        {
            var panelGame = CreatPanel(panelName, panel);
            panelGame.Bengin(value);
            return panelGame;
        }

        public static BaseUI ShowUIPanel<T, G>(G value, PanelHierarchy panel = default)
        {
            var panelGame = CreatPanel(typeof(T).ToString(), panel);
            panelGame.Bengin(value);
            return panelGame;
        }

        public static BaseUI ShowUIPanelMost<T>()
        {
            return ShowUIPanel<T>(PanelHierarchy.TopMost);
        }

        public static BaseUI ShowUIPanelMost(string str)
        {
            return ShowUIPanel(str, PanelHierarchy.TopMost);
        }

        private static BaseUI CreatPanel(string panelName, PanelHierarchy panel = default)
        {
            panelName = CommonFunction.ReplaceEZ(panelName);
            CloseUIPanel(panelName);
            BaseUI baseUI = Instantiate(Resources.Load<BaseUI>(panelName));
            PanelHierarchy hierarchy = panel != default ? panel : baseUI.Hierarchy;
            baseUI.transform.name = panelName;
            baseUI.transform.SetParent(HierarchyArray[hierarchy.ToString()], false);
            AllPanel.Add(panelName, baseUI);
            return baseUI;
        }

        public static void CloseUIPanel<T>()
        {
            DsetoryUIPanel(typeof(T).ToString());
        }

        public static void CloseUIPanel(string panelName)
        {
            DsetoryUIPanel(panelName);
        }

        private static void DsetoryUIPanel(string panelName)
        {
            panelName = CommonFunction.ReplaceEZ(panelName);
            if (AllPanel.ContainsKey(panelName))
            {
                if (AllPanel[panelName] != null)
                {
                    Destroy(AllPanel[panelName].gameObject);
                }
                AllPanel.Remove(panelName);
            }
        }

        #endregion

        #region 指定方法

        public static void ShowChooseConfirm(string value, UnityAction callBack = null)
        {
            Hashtable hashtable = new Hashtable();
            hashtable["value"] = value;
            hashtable["callBack"] = callBack;
            ShowUIPanel<ChooseConfirm, Hashtable>(hashtable);
        }

        public static void ShowConfirmHint(string value)
        {
            ShowUIPanel<ConfirmHint, string>(value);
        }

        #endregion
    }
}