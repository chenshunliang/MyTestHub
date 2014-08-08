using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace IMChat.Common
{
    //日志记录
    public static class LogHelper
    {
        public static void WriteLog(string text)
        {
            ILog log = LogManager.GetLogger("LogHelper");
            //log.Error(text);
            log.Fatal(text);
        }
    }
}