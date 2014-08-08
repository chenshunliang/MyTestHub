using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrmTest.Entity
{
    public class Map : Hashtable
    {
        public void Put(object key, object value)
        {
            this.Add(key, value);
        }
    }
}