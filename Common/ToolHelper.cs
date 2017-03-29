using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ToolHelper
    {
        /// <summary>    
        /// 将c# DateTime时间格式转换为Unix时间戳格式    
        /// </summary>    
        /// <param name="time">时间</param>    
        /// <returns>long</returns>    
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位        
            return t;
        }

        /// <summary>          
        /// 时间戳转为C#格式时间          
        /// </summary>          
        /// <param name=”timeStamp”></param>          
        /// <returns></returns>          
        public static DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 得到当前时间的时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            return ConvertDateTimeToInt(DateTime.Now);
        }

        /// <summary>
        /// 生成唯一Id
        /// </summary>
        /// <returns></returns>
        public static string GetGuidStr()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
