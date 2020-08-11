using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZFramework
{
    public class BaseUI : MonoBehaviour
    {
        public PanelHierarchy Hierarchy = PanelHierarchy.Middel;

        public virtual void Bengin<T>(T value) { }

    }
}
