using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrmTest.Entity
{
    public class TableInfo
    {
        //存放表名的变量
        private string tableName;

        //存放主键生成方式的变量
        private int strategy;

        //存放Id列名和列值的对象
        private IdInfo id = new IdInfo();

        //存放列名和列值的集合
        private ColumnInfo columns = new ColumnInfo();

        //存放属性名和列名对象关系的集合
        private Map propToColumn = new Map();

        public Map PropToColumn
        {
            get { return propToColumn; }
            set { propToColumn = value; }
        }

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        public int Strategy
        {
            get { return strategy; }
            set { strategy = value; }
        }


        public IdInfo Id
        {
            get { return id; }
            set { id = value; }
        }

        public ColumnInfo Columns
        {
            get { return columns; }
            set { columns = value; }
        }
    }
}