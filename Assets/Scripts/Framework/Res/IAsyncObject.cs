using System;

namespace ZXC.Res
{
    public interface IAsyncObject
    {
        bool IsCompleted { get; }
        bool IsSuccess { get; }
        bool IsError { get; }
        string ErrorMsg { get; }
        float Progress { get; }
    }
}