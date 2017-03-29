using Aliyun.OpenServices.OpenStorageService;
using DemoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class OSSHelper
    {
        private readonly string accessId = "LTAIGRprdMxujeVq";
        private readonly string accessKey = "b4tbXxYVNw6jzAOZD4iiBf20UjcXG1 ";
        private readonly string bucketurl = "sdpbucket.oss-cn-shanghai.aliyuncs.com";
        private OssClient ossClient;
        public OSSHelper()
        {
        }
    }
}
