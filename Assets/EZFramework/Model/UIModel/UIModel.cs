using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZFramework
{
    public partial class UIModel : BaseModel
    {
        private static Dictionary<string, Transform> HierarchyArray = new Dictionary<string, Transform>();

        public override void Begin()
        {
            CreatHierarchy();
        }

        public override void End()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            TopMostControl();
        }

        private void CreatHierarchy()
        {
            string[] nameList = System.Enum.GetNames(typeof(PanelHierarchy));
            for (int i = 0; i < nameList.Length; i++)
            {
                GameObject panelHierarchy = Instantiate(Resources.Load("PanelHierarchy"), transform.Find("Canvas")) as GameObject;
                panelHierarchy.name = nameList[i];
                HierarchyArray.Add(panelHierarchy.name, panelHierarchy.transform);
            }

            Shade = Instantiate(Resources.Load("Shade") as GameObject).transform;
            Shade.SetParent(HierarchyArray[PanelHierarchy.TopMost.ToString()], false);
        }

        /// <summary>
        /// 最顶层遮罩控制
        /// </summary>
        private Transform Shade;
        private void TopMostControl()
        {
            bool isActive = HierarchyArray[PanelHierarchy.TopMost.ToString()].childCount > 1;
            Shade.gameObject.SetActive(isActive);
            if (isActive)
            {
                Shade.SetSiblingIndex(HierarchyArray[PanelHierarchy.TopMost.ToString()].childCount - 2);
            }
        }
    }
    public enum PanelHierarchy
    {
        Lower,
        Middel,
        Topper,
        TopMost
    }
}

