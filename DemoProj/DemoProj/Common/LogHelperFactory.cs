using Common.Model;
using Common.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class LogHelperFactory
    {
        public LogHelperFactory(){}

        public void WriteLog(ResponseModel _responseModel, string _msg, DemoProj.Models.EnumModel.Errorflag _errorflag) {
            if (string.IsNullOrEmpty(GetConfigValue("OpenLogFlag"))) {
                return;
            }

            //是否打开日志记录
            if (bool.Parse(GetConfigValue("OpenLogFlag"))) {
                //【Log4Net】
                if (bool.Parse(GetConfigValue("Log4Net"))){
                    Log4NetHelper.WriteLog(_errorflag, _msg);
                }

                //【Redis】
                if (bool.Parse(GetConfigValue("Redis")))
                {
                    using (RedisHelper _redisHelper = new RedisHelper()) { 
                        _redisHelper.Set(_responseModel.Host, _responseModel.TimeStamp.ToString(), _responseModel);
                        _redisHelper.Get<ResponseModel>(_responseModel.Host, _responseModel.TimeStamp.ToString());
                    }
                }
            }
        }

        private string GetConfigValue(string ConfigKey) {
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\LogHelper.config";
            ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
            ecf.ExeConfigFilename = ConfigPath;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
            return config.AppSettings.Settings[ConfigKey].Value;
        }

    }
}