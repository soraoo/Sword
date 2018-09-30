using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXC.Res
{
    public interface IAssetLoader : IAsyncObject, IDisposable
    {
        UnityEngine.Object Asset { get; }
        void Init(AssetId assetId);
        void LoadAsync<T>(LoadAssetDelegate<T> onFinished) where T : UnityEngine.Object;
    }
}