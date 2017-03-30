using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DemoProj.Models.EnumModel;

namespace Common
{
    public class Log4NetHelper
    {
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="_msg">内容</param>
        #region 
        public static void WriteLog(Errorflag _errorflag, string _msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("MyGetLogger");
            switch (_errorflag) {
                case Errorflag.Error:
                    log.Error(_msg);break;
                case Errorflag.Fatal:
                    log.Fatal(_msg); break;
                case Errorflag.Info:
                    log.Info(_msg); break;
                case Errorflag.Debug:
                    log.Debug(_msg); break;
                case Errorflag.Warn:
                    log.Warn(_msg); break;
                default:break;
            }
        }
        #endregion
    }
}
