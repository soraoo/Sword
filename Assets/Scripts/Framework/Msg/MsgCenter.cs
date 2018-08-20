using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXC
{
    public delegate void OnMsgHandler<T>(T args);

    public class MsgCenter<T> : ZSingleton<MsgCenter<T>>
    {
        private Dictionary<uint, List<OnMsgHandler<T>>> msgHandlerDic;

        private MsgCenter()
        {
            msgHandlerDic =new Dictionary<uint, List<OnMsgHandler<T>>>();
        }

        public void RegisterMsg(uint msg, OnMsgHandler<T> handler)
        {
            if (msgHandlerDic.ContainsKey(msg))
            {
                if (msgHandlerDic[msg].Contains(handler))
                    return;
                msgHandlerDic[msg].Add(handler);
            }
            else
            {
                msgHandlerDic.Add(msg, new List<OnMsgHandler<T>> { handler });
            }
        }

        public void UnRegisterMsg(uint msg, OnMsgHandler<T> handler)
        {
            if (!msgHandlerDic.ContainsKey(msg))
                return;
            foreach (var handlerItem in msgHandlerDic[msg])
            {
                if (handlerItem == handler)
                {
                    msgHandlerDic[msg].Remove(handler);
                    if (msgHandlerDic[msg].Count == 0)
                        msgHandlerDic.Remove(msg);
                    return;
                }
            }
        }

        public void SendMsg(uint msg, T args)
        {
            if (!msgHandlerDic.ContainsKey(msg))
                return;
            foreach (var handlerItem in msgHandlerDic[msg])
            {
                if(handlerItem != null)
                    handlerItem(args);
            }
        }
    }
}