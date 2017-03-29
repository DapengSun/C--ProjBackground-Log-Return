using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class ResponseModel
    {
        public string Host { get; set; }
        public long TimeStamp { get; set; }
        public Guid Token { get; set; }
        public string MethodName { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public DemoProj.Models.EnumModel.Errorflag Errorflag { get; set; }
        public string Message { get; set; }
        public string ResponseResult { get; set; }

    }
}
