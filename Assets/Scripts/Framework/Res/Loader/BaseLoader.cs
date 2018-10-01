using System;
using System.Collections;
using System.Collections.Generic;

namespace ZXC.Res
{
    public abstract class BaseLoader : IAsyncObject, IDisposable
    {
        #region static Factory methods

        private static Dictionary<Type, Dictionary<string, BaseLoader>> loaderPoolDic = new Dictionary<Type, Dictionary<string, BaseLoader>>();

        public static T CreateLoader<T, U>(string path, LoadDelegate onFinished)
            where T : BaseLoader
        {
            Dictionary<string, BaseLoader> loaderDic = GetLoaderDic(typeof(T));
            BaseLoader loader = null;
            if(!loaderDic.TryGetValue(path, out loader))
            {
                loader = ObjectFactory.GetFactory(FactoryType.Pool).CreateObject<BaseLoader>();
                loaderDic[path] = loader;
                loader.Init(onFinished);
            }
            loader.RefCount++;
            return loader as T;
        }

        public static Dictionary<string, BaseLoader> GetLoaderDic(Type type)
        {
            Dictionary<string, BaseLoader> loaderDic = null;
            if(!loaderPoolDic.TryGetValue(type, out loaderDic))
            {
                loaderDic = loaderPoolDic[type] = new Dictionary<string, BaseLoader>();
            }
            return loaderDic;
        }

        #endregion

        public object ResultObject { get; protected set; }
        public bool IsCompleted
        {
            get
            {
                return IsDone();
            }
        }

        public bool IsSuccess
        {
            get
            {
                return IsCompleted && !IsError;
            }
        }

        public bool IsError
        {
            get
            {
                return !string.IsNullOrEmpty(Error());
            }
        }

        public float Progress
        {
            get
            {
                return GetProgress();
            }
        }

        public string ErrorMsg
        {
            get
            {
                return Error();
            }
        }

        public int RefCount
        {
            get; private set;
        }

        public virtual void Dispose()
        {
            ResultObject = null;
        }

        protected virtual void Init(LoadDelegate onFinished)
        {
            ResultObject = null;
        }

        abstract protected bool IsDone();
        abstract protected string Error();
        abstract protected float GetProgress();
    }
}