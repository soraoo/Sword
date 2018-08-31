using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXC.Factory
{
    public interface IObjectFactory
    {
        T CreateObject<T>() where T : class;
        void ReleaseObject(object obj);
    }
}