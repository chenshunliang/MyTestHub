using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrmTest.MapAttr
{

    //表映射
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableAttribute : Attribute
    {
        private string _name = string.Empty;

        public TableAttribute() { }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}