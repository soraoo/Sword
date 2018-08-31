using System;
using System.Collections;

namespace ZXC.Res
{
    public abstract class BaseLoader : IAsyncObject, IDisposable
    {
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

        public virtual void Dispose()
        {
        }

        abstract protected bool IsDone();
        abstract protected string Error();
        abstract protected float GetProgress();
    }
}