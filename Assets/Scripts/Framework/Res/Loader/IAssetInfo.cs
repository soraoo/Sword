using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXC.Res;

namespace ZXC
{
    public interface IAssetInfo<T> : IAsyncObject, IDisposable
    {
        string AssetPath { get; }
        T Asset { get; }
    }
}