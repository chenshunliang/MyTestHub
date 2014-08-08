using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrmTest.MapAttr
{

    //列映射
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class ColumnAttribute : Attribute
    {
        private string _Name = string.Empty; //列名
        private bool _IsUnique = false; //是否唯一
        private bool _IsNull = true; //是否允许为空
        private bool _IsInsert = true; //是否插入到表中
        private bool _IsUpdate = true; //是否修改到表中

        public ColumnAttribute() { }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public bool IsUnique
        {
            get { return _IsUnique; }
            set { _IsUnique = value; }
        }
        public bool IsNull
        {
            get { return _IsNull; }
            set { _IsNull = value; }
        }
        public bool IsInsert
        {
            get { return _IsInsert; }
            set { _IsInsert = value; }
        }
        public bool IsUpdate
        {
            get { return _IsUpdate; }
            set { _IsUpdate = value; }
        }
    }
}