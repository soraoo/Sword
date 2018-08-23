using System;
using System.Collections.Generic;

namespace ZXC
{
    /// <summary>
    /// 链式调度类
    /// </summary>
    public class Chain
    {
        private delegate void WaitNextDelegate(Action next, Action kill);
        private Queue<WaitNextDelegate> chainQueue;
        public bool IsFinished { get; private set; }
        private bool canNext;

        #region API

        public Chain Start()
        {
            var chain = new Chain();
            return chain;
        }

        public Chain Start(Action callback)
        {
            var chain = new Chain();
            return chain.Then(callback);
        }

        public Chain Start(ChainThenDelegate callback)
        {
            var chain = new Chain();
            return chain.Then(callback);
        }

        public Chain Then(Action callback)
        {
            WaitNext((next, kill) =>
            {
                callback();
                next();
            });
            return this;
        }

        public Chain Then(ChainThenDelegate callback)
        {
            WaitNext((next, kill) =>
            {
                callback(next);
            });
            return this;
        }

        public Chain Then(ChainThenDelegateWithKill callback)
        {
            WaitNext((next, kill) =>
            {
                callback(next, kill);
            });
            return this;
        }

        #endregion

        private Chain()
        {
            canNext = true;
        }

        private void WaitNext(WaitNextDelegate callback)
        {
            if (!canNext)
            {
                if (chainQueue == null)
                {
                    chainQueue = new Queue<WaitNextDelegate>();
                }
                chainQueue.Enqueue(callback);
            }
            else
            {
                canNext = false;
                callback(Next, Kill);
            }
        }

        private void Next()
        {
            canNext = true;
            if (chainQueue != null && chainQueue.Count != 0)
            {
                WaitNext(chainQueue.Dequeue());
            }
            else
            {
                IsFinished = true;
                Kill();
            }
        }

        private void Kill()
        {
            canNext = true;
            if (chainQueue != null)
            {
                chainQueue.Clear();
                chainQueue = null;
            }
        }
    }
}