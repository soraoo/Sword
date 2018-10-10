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

        private string bundleId;

        /// <summary>
        /// 释放引用
        /// </summary>
        public void Dispose()
        {
            bundleId = null;
            Asset = null;
            OnDispose();
        }

        public void Init(string assetId)
        {
            this.bundleId = assetId;
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
                .Coroutine(DoLoadAssetBundleAsync())
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

        private IEnumerator DoLoadAssetBundleAsync()
        {
            var request = AssetBundle.LoadFromFileAsync(bundleId);
            while(!request.isDone)
            {
                Progress = request.progress;
                yield return null;
            }
            Progress = 1f;
            IsCompleted = request.isDone;
            Asset = request.assetBundle;
            IsSuccess = Asset != null;
            IsError = Asset == null;
            if(IsError && Asset == null)
            {
                ErrorMsg = "load asset is null";
            }
        }
    }
}