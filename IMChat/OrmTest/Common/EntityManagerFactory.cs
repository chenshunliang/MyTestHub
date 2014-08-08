using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrmTest.Common
{
    public class EntityManagerFactory
    {
        public static IEntityManager CreateEntityManager()
        {
            return new EntityManagerImpl();
        }
    }
}