using EZFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZFramework
{
    public class BaseData<T> where T : BaseData<T>
    {
        public void Save()
        {
            SingleData<T>.SaveData(SingleData<T>.Data);
        }
    }
}