using System;

namespace ZXC
{
    #region MsgHandler

    public delegate void OnMsgHandler();
    public delegate void OnMsgHandler<T>(T args);
    public delegate void OnMsgHandler<T, U>(T argsT, U argsU);
    public delegate void OnMsgHandler<T, U, V>(T argsT, U argsU, V argsV);

    #endregion

    #region Chain Callback

    public delegate void ChainThenDelegate(Action next);
    public delegate void ChainThenDelegateWithKill(Action next, Action kill);

    #endregion
}