using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OrmTest.Common
{
    public class TransactionManager
    {
        public static IDbTransaction CreateTransaction()
        {
            return DbFactory.CreateDbTransaction();
        }
    }
}