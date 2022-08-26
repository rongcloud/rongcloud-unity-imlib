using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn_rongcloud_im_unity_example
{
    internal class Util
    {
        /// <summary>
        /// 当前 UTC 时间戳（毫秒）
        /// </summary>
        /// <returns></returns>
        public static Int64 CurrentTimeStamp()
        {
            var epoch = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            return epoch;
        }

        public static string FormatTs(Int64 timestamp)
        {
            var startDt = new DateTime(1970, 1, 1, 0, 0, 0);
            startDt = startDt.AddMilliseconds(timestamp);
            return startDt.ToLocalTime().ToString("G");
        }
    }
}
