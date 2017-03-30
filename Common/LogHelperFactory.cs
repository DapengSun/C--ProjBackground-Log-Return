using Common.Model;
using Common.Redis;
using DemoProj.Models;
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

            try
            {
                //是否打开日志记录
                if (bool.Parse(GetConfigValue("OpenLogFlag")))
                {
                    //【Log4Net】
                    if (bool.Parse(GetConfigValue("Log4Net")))
                    {
                        Log4NetHelper.WriteLog(_errorflag, _msg, EnumModel.LogType.Application);
                    }

                    //【Redis】
                    if (bool.Parse(GetConfigValue("Redis")))
                    {
                        using (RedisHelper _redisHelper = new RedisHelper())
                        {
                            _redisHelper.Set(_responseModel.Host, _responseModel.TimeStamp.ToString(), _responseModel);
                            _redisHelper.Get<ResponseModel>(_responseModel.Host, _responseModel.TimeStamp.ToString());
                        }
                    }
                }
            }
            catch (Exception ee) {
                Log4NetHelper.WriteLog(EnumModel.Errorflag.Error, ee.ToString(), EnumModel.LogType.System);
            }
        }

        /// <summary>
        /// 通过配置Key 获得是否数据库是否开启 
        /// </summary>
        /// <param name="ConfigKey"></param>
        /// <returns></returns>
        private string GetConfigValue(string ConfigKey) {
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"\LogHelper.config";
            ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
            ecf.ExeConfigFilename = ConfigPath;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
            return config.AppSettings.Settings[ConfigKey].Value;
        }
    }
}