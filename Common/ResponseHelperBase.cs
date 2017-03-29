using Common.Model;
using Common.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class ResponseHelperBase : IDisposable
    {
        protected string IpAddress { get; set; }
        protected Guid Token { get; set; }
        protected HttpStatusCode HttpStatusCode { get; set; }
        protected string Message { get; set; }
        protected Object ResponseResult { get; set; }
        protected long TimeStamp { get; set; }

        public ResponseHelperBase() {}

        public abstract string ReturnResultAndLog(string _ipAddress,Guid _token,HttpStatusCode _httpStatusCode, DemoProj.Models.EnumModel.Errorflag _errorflag, string _message,Object _responseResult);

        /// <summary>
        /// 获得处理的方法名称
        /// </summary>
        /// <param name="deep"></param>
        /// <returns></returns>
        public string GetMethodName(int deep = 1)
        {
            GetMethodStart:
            // 这里忽略1层堆栈，也就忽略了当前方法GetMethodName，这样拿到的就正好是外部调用GetMethodName的方法信息
            var method = new StackFrame(deep).GetMethod(); 

            try
            {

                var property = (
                          from p in method.DeclaringType.GetProperties(
                                   BindingFlags.Instance |
                                   BindingFlags.Static |
                                   BindingFlags.Public |
                                   BindingFlags.NonPublic)
                          where p.GetGetMethod(true) == method || p.GetSetMethod(true) == method
                          select p).FirstOrDefault();
                return method.ReflectedType.Name + "-" + (property == null ? method.Name : property.Name);
            }
            catch
            {
                if (deep == -1)
                {
                    return "error null method!";
                }
                deep--;
                goto GetMethodStart;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~ResponseHelperBase() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
