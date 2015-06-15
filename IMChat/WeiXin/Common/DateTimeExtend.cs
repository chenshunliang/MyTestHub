using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXin.Common
{
    public static class DateTimeExtend
    {
        public static int DateTimeToInt(this DateTime dt)
        {
            TimeSpan ts = dt - Convert.ToDateTime("1970-1-1 8:00:00");
            int intervel = (int)ts.TotalSeconds;
            return intervel;
        }
    }
}