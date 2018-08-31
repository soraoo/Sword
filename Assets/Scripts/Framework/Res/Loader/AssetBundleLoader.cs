using System;
using System.Collections;
using UnityEngine;

namespace ZXC.Res
{
    public class AssetBundleLoader : BaseLoader
    {
        public AssetBundle assetBundle
        {
            get
            {
                return ResultObject as AssetBundle;
            }
        }

        protected override void Init()
        {
            
        }

        protected override bool IsDone()
        {
            return false;
        }

        protected override string Error()
        {
            return "";
        }

        protected override float GetProgress()
        {
            return 0f;
        }

        public override void Dispose()
        {

        }
    }
}