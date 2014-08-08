using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrmTest.Entity
{
    public class IdInfo
    {
        private string key;
        private object value;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}