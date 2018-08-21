using System;

namespace ZXC
{
    public delegate void OnMsgHandler();
    public delegate void OnMsgHandler<T>(T args);
    public delegate void OnMsgHandler<T, U>(T argsT, U argsU);
    public delegate void OnMsgHandler<T, U, V>(T argsT, U argsU, V argsV);
}