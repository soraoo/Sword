using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ZXC
{
    public class ResMgr : ZMonoSingleton<ResMgr>
    {
        private HashSet<string> permanentAssetBundleHashSet;
        private Dictionary<string, AssetBundle> cacheAssetBundleDic;
        private Dictionary<string, Object> cacheResDic;

        public override IEnumerator Init()
        {
            //缓存全局ab
            yield return base.Init();
        }

        public void LoadAsset<T>(AssetId assetId, LoadAssetDelegate<T> onFinished) where T : Object
        {
            Object asset = null;
            if (cacheResDic.TryGetValue(assetId.ResId, out asset))
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

        protected override void AfterAwake()
        {
            permanentAssetBundleHashSet = new HashSet<string>();
            cacheResDic = new Dictionary<string, Object>();
            cacheAssetBundleDic = new Dictionary<string, AssetBundle>();
        }

        // private IEnumerator DoInit()
        // {
        //     //load asset bundle mainifest
        //     AssetBundle assetBundle = AssetBundle.LoadFromFile(string.Format("{0}/{1}", ResUtility.GetAssetBundlesPath(), ResUtility.ASSET_BUNDLE_FOLDER_NAME));
        //     AssetBundleManifest manifest = assetBundle.LoadAsset<AssetBundleManifest>(ResUtility.ASSET_BUNDLE_MANIFEST);
        // }

        private void LoadAssetFromLocal<T>(AssetId assetId, LoadAssetDelegate<T> onFinished) where T : Object
        {
            T asset = null;
            bool isSuccess = false;
            string errMsg = string.Empty;
            try
            {
                asset = AssetDatabase.LoadAssetAtPath<T>(assetId.ToString());
            }
            catch (System.Exception e)
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