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
    public class ResponseHelper : ResponseHelperBase
    { 
        public ResponseHelper() { }

        public override string ReturnResultAndLog(string _ipAddress, Guid _token, HttpStatusCode _httpStatusCode, 
            DemoProj.Models.EnumModel.Errorflag _errorflag, string _message, Object _responseResult)
        {
            this.IpAddress = _ipAddress;
            this.Token = _token;
            this.HttpStatusCode = _httpStatusCode;
            this.Message = _message;
            this.ResponseResult = _responseResult;
            this.TimeStamp = ToolHelper.GetTimeStamp();

            #region 返回内容
            //返回内容实体ResponseModel
            ResponseModel _responseModel = new ResponseModel()
            {
                Host = this.IpAddress,
                TimeStamp = this.TimeStamp,
                Token = this.Token,
                MethodName = this.GetMethodName(4),
                HttpStatusCode = this.HttpStatusCode,
                Errorflag = _errorflag,
                Message = this.Message,
                ResponseResult = Common.Convert.JsonConvert(this.ResponseResult)
            };

            //序列化 转换成Json串格式 返回Controller
            string _msg = Common.Convert.JsonConvert(_responseModel);
            #endregion

            #region 写入日志
            new LogHelperFactory().WriteLog(_responseModel, _msg, _errorflag);
            #endregion

            return _msg;
        }
    }
}
