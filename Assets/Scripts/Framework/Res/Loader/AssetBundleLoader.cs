using System;
using System.Collections;
using UnityEngine;

namespace ZXC.Res
{
    public class AssetBundleLoader : IAssetLoader
    {
        public UnityEngine.Object Asset { get; private set; }

        public bool IsCompleted { get; private set; }

        public bool IsSuccess { get; private set; }

        public bool IsError { get; private set; }

        public string ErrorMsg { get; private set; }

        public float Progress { get; private set; }

        public object ResultObject { get { return Asset; } }

        private AssetId assetId;

        /// <summary>
        /// 释放引用
        /// </summary>
        public void Dispose()
        {
            assetId = null;
            Asset = null;
            OnDispose();
        }

        public void Init(AssetId assetId)
        {
            this.assetId = assetId;
            IsCompleted = false;
            IsSuccess = false;
            IsError = false;
            ErrorMsg = string.Empty;
            Progress = 0f;
        }

        public void LoadAsync<T>(LoadAssetDelegate<T> onFinished)
            where T : UnityEngine.Object
        {
            Chain.Start()
                .Coroutine(DoLoadAssetBundleAsync<T>())
                .Then(next =>
                {
                    onFinished(IsSuccess, ErrorMsg, Asset as T);
                    next();
                });
        }

        /// <summary>
        /// 子类析构
        /// </summary>
        protected void OnDispose()
        {

        }

        private IEnumerator DoLoadAssetBundleAsync<T>()
        {
            yield return null;
        }
    }
}