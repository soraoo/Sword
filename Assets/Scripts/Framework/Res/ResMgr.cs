using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ZXC
{
    public class ResMgr : ZMonoSingleton<ResMgr>
    {
        private Dictionary<string, Object> cacheResDic;
        private Dictionary<string, AssetBundle> cacheAssetBundleDic;

        protected override void AfterAwake()
        {
            cacheResDic = new Dictionary<string, Object>();
            cacheAssetBundleDic = new Dictionary<string, AssetBundle>();
        }

        public void LoadAsset<T>(AssetId assetId, LoadAssetDelegate<T> onFinished) where T : Object
        {
            Object asset = null;
            if(cacheResDic.TryGetValue(assetId.ResId, out asset))
            {
                onFinished(true, string.Empty, asset as T);
            }
            else
            {
#if UNITY_EDITORF
                LoadAssetFromLocal<T>(assetId, onFinished);
#else
                LoadAssetFromBundle<T>(assetId, onFinished);
#endif
            }
        }
        
        private void LoadAssetFromLocal<T>(AssetId assetId, LoadAssetDelegate<T> onFinished) where T : Object
        {
            T asset = null;
            bool isSuccess = false;
            string errMsg = string.Empty;
            try
            {
               asset  = AssetDatabase.LoadAssetAtPath<T>(assetId.ToString());
            }
            catch(System.Exception e)
            {
                isSuccess = false;
                errMsg = e.ToString();
            }
            finally
            {
                onFinished(isSuccess, errMsg, asset);
            }
        }

        private void LoadAssetFromBundle<T>(AssetId assetId, LoadAssetDelegate<T> onFinished) where T : Object
        {

        }
    }
}