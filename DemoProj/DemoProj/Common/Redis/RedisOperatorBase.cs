using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;

namespace Common.Redis
{
    /// <summary>
    /// RedisOperatorBase类，是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    /// </summary>
    public abstract class RedisOperatorBase : IDisposable
    {
        protected IRedisClient redisClient { get; private set; }
        private bool _disposed = false;
        protected RedisOperatorBase()
        {
            redisClient = RedisManager.GetClient();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    redisClient.Dispose();
                    redisClient = null;
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            redisClient.Save();
        }
        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            redisClient.SaveAsync();
        }
    }
}
