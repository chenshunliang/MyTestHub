using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrmTest.Common
{
    public class DbTypeConvert
    {
        static decimal ToDecimal(int value)
        {
            return Convert.ToDecimal(value);
        }
    }
}