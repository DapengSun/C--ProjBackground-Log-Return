using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoProj.Models
{
    public class EnumModel
    {
        /// <summary>
        /// 错误标识
        /// </summary>
        public enum Errorflag {
            //错误
            Error,
            //严重错误
            Fatal,
            //一般信息
            Info,
            //调试信息
            Debug,
            //警告信息
            Warn
        }

        /// <summary>
        /// 日志类别
        /// </summary>
        public enum LogType {
            //应用日志
            Application,
            //系统日志
            System
        }
    }
}