using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoProj.Models
{
    public class EnumModel
    {
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
    }
}