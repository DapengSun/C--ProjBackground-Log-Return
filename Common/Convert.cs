using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Convert
    {
        public static string JsonConvert(Object _obj) {
            return Newtonsoft.Json.JsonConvert.SerializeObject(_obj);
        }

        public static T ObjectConvert<T>(string _obj)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(_obj);
        }
    }
}
