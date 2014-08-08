using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace WebTest
{
    public static class LogRecord
    {
        //记录日志
        public static void Log(string str)
        {
            ILog log = LogManager.GetLogger(typeof(LogRecord));
            log.Fatal(str);
        }
    }
}