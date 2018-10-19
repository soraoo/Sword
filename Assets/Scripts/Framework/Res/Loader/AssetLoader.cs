using System;
using System.Collections;
using UnityEngine;
using UnityFx.Async;
using ZXC;

namespace ZXC.Res
{
    public class AssetLoader<T> : IDisposable where T : UnityEngine.Object
    {
        private class AssetInfo<TInfo> : IAssetInfo<TInfo> where TInfo : UnityEngine.Object
        {
            public string AssetPath { get; set; }

            public bool IsCompleted { get; set; }

            public bool IsSuccess { get; set; }

            public bool IsError { get; set; }

            public string ErrorMsg { get; set; }

            public float Progress { get; set; }

            public object ResultObject { get { return Asset; } }

            public TInfo Asset { get; set; }

            public void Dispose()
            {
                Asset = null;
            }
        }

        private AssetInfo<T> assetInfo;
        private AsyncCompletionSource<T> asc;

        /// <summary>
        /// 释放引用
        /// </summary>
        public void Dispose()
        {
            assetInfo = null;
            asc = null;
            OnDispose();
        }

        protected void Init(string assetPath)
        {
            assetInfo = new AssetInfo<T>();
            assetInfo.AssetPath = assetPath;
            assetInfo.IsCompleted = false;
            assetInfo.IsSuccess = false;
            assetInfo.IsError = false;
            assetInfo.ErrorMsg = string.Empty;
            assetInfo.Progress = 0f;
        }

        public IAsyncOperation<T> LoadAssetBundle(string assetPath)
        {
            return LoadAsync(assetPath, null, DoLoadAssetBundleAsync());
        }

        public IAsyncOperation<T> LoadAsset(string assetPath, AssetBundle assetBundle)
        {
            return LoadAsync(assetPath, assetBundle, DoLoadAssetAsync(assetBundle));
        }

        public IAsyncOperation<T> LoadAllAsset(AssetBundle assetBundle)
        {
            return LoadAsync(string.Empty, assetBundle, DoLoadAllAssetBundle(assetBundle));
        }

        private IAsyncOperation<T> LoadAsync(string path, AssetBundle assetBundle, IEnumerator itor)
        {
            if (assetInfo == null)
            {
                Init(path);
            }
            asc = new AsyncCompletionSource<T>();
            Chain.Start()
                .Coroutine(itor);
            return asc;
        }

        /// <summary>
        /// 子类析构
        /// </summary>
        protected void OnDispose()
        {

        }

        private IEnumerator DoLoadAssetBundleAsync()
        {
            var request = AssetBundle.LoadFromFileAsync(assetInfo.AssetPath);
            while (!request.isDone)
            {
                assetInfo.Progress = request.progress;
                asc.SetProgress(assetInfo.Progress);
                yield return null;
            }
            assetInfo.Progress = 1f;
            asc.SetProgress(assetInfo.Progress);
            assetInfo.IsCompleted = request.isDone;
            assetInfo.Asset = request.assetBundle as T;
            assetInfo.IsSuccess = assetInfo.Asset != null;
            assetInfo.IsError = assetInfo.Asset == null;
            if (assetInfo.IsError && assetInfo.Asset == null)
            {
                assetInfo.ErrorMsg = $"load assetBundle {assetInfo.Asset.name} is null";
                asc.SetException(new AssetLoaderException(assetInfo.ErrorMsg));
            }
            else
            {
                asc.SetResult(assetInfo.Asset);
            }
        }

        private IEnumerator DoLoadAssetAsync(AssetBundle assetBundle)
        {
            var request = assetBundle.LoadAssetAsync(assetInfo.AssetPath);
            while (!request.isDone)
            {
                assetInfo.Progress = request.progress;
                asc.SetProgress(assetInfo.Progress);
                yield return null;
            }
            assetInfo.Progress = 1f;
            asc.SetProgress(assetInfo.Progress);
            assetInfo.IsCompleted = request.isDone;
            assetInfo.Asset = request.asset as T;
            assetInfo.IsSuccess = assetInfo.Asset != null;
            assetInfo.IsError = assetInfo.Asset == null;
            if (assetInfo.IsError && assetInfo.Asset == null)
            {
                assetInfo.ErrorMsg = $"load asset {assetInfo.Asset.name} is null";
                asc.SetException(new AssetLoaderException(assetInfo.ErrorMsg));
            }
            else
            {
                asc.SetResult(assetInfo.Asset);
            }
        }

        private IEnumerator DoLoadAllAssetBundle(AssetBundle assetBundle)
        {
            var request = assetBundle.LoadAllAssetsAsync<T>();
            while (!request.isDone)
            {
                assetInfo.Progress = request.progress;
                asc.SetProgress(assetInfo.Progress);
                yield return null;
            }
            assetInfo.Progress = 1f;
            asc.SetProgress(assetInfo.Progress);
            assetInfo.IsCompleted = request.isDone;
            assetInfo.Asset = request.allAssets as T;
            assetInfo.IsSuccess = assetInfo.Asset != null;
            assetInfo.IsError = assetInfo.Asset == null;
            if (assetInfo.IsError && assetInfo.Asset == null)
            {
                assetInfo.ErrorMsg = $"load all asset in {assetInfo.AssetPath} is null";
                asc.SetException(new AssetLoaderException(assetInfo.ErrorMsg));
            }
            else
            {
                asc.SetResult(assetInfo.Asset);
            }
        }
    }
}