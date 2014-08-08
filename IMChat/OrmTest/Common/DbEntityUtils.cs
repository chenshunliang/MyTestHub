using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using OrmTest.Entity;
using OrmTest.MapAttr;

namespace OrmTest.Common
{
    public class DbEntityUtils
    {
        public static string GetTableName(Type classType)
        {
            string strTableName = string.Empty;
            string strEntityName = string.Empty;

            strEntityName = classType.FullName;

            object classAttr = classType.GetCustomAttributes(false)[0];
            if (classAttr is TableAttribute)
            {
                TableAttribute tableAttr = classAttr as TableAttribute;
                strTableName = tableAttr.Name;
            }
            if (string.IsNullOrEmpty(strTableName))
            {
                throw new Exception("实体类:" + strEntityName + "的属性配置[Table(name=\"tablename\")]错误或未配置");
            }

            return strTableName;
        }

        public static string GetPrimaryKey(object attribute)
        {
            string strPrimary = string.Empty;
            IdAttribute attr = attribute as IdAttribute;
            switch (attr.Strategy)
            {
                case GenerationType.INDENTITY:
                    break;
                case GenerationType.SEQUENCE:
                    strPrimary = System.Guid.NewGuid().ToString();
                    break;
                case GenerationType.TABLE:
                    break;
            }

            return strPrimary;
        }

        public static string GetColumnName(object attribute)
        {
            string columnName = string.Empty;
            if (attribute is ColumnAttribute)
            {
                ColumnAttribute columnAttr = attribute as ColumnAttribute;
                columnName = columnAttr.Name;
            }
            if (attribute is IdAttribute)
            {
                IdAttribute idAttr = attribute as IdAttribute;
                columnName = idAttr.Name;
            }

            return columnName;
        }

        public static TableInfo GetTableInfo(object entity, DbOperateType dbOpType)
        {
            bool breakForeach = false;//是否跳出forach循环
            string strPrimaryKey = string.Empty;//主键变量 
            TableInfo tableInfo = new TableInfo();    //存储表信息的对象       
            Type type = entity.GetType();//获取实体对象类型

            tableInfo.TableName = GetTableName(type);//获取表名
            PropertyInfo[] properties = ReflectionUtils.GetProperties(type);
            foreach (PropertyInfo property in properties)
            {
                object propvalue = null;
                string columnName = string.Empty;//列名(数据库表结构)
                string propName = columnName = property.Name;
                if (dbOpType != DbOperateType.SELECT)
                    propvalue = ReflectionUtils.GetPropertyValue(entity, property);

                object[] propertyAttrs = property.GetCustomAttributes(false);
                for (int i = 0; i < propertyAttrs.Length; i++)
                {
                    object propertyAttr = propertyAttrs[i];
                    if (DbEntityUtils.IsCaseColumn(propertyAttr, dbOpType))
                    {
                        breakForeach = true; break;
                    }

                    string tempVal = GetColumnName(propertyAttr);
                    columnName = tempVal == string.Empty ? propName : tempVal;

                    if (propertyAttr is IdAttribute)
                    {
                        if (dbOpType == DbOperateType.INSERT)
                        {
                            //获取主键生成方式，存入tableInfo.Strategy属性中
                            IdAttribute idAttr = propertyAttr as IdAttribute;
                            tableInfo.Strategy = idAttr.Strategy;

                            //如果是插入操作，且主键ID值为空，则根据主键生成策略生成主键值，
                            //默认生成策略为自动增长，这种情况不处理，有数据库来处理
                            if (CommonUtils.IsNullOrEmpty(propvalue))
                            {
                                strPrimaryKey = DbEntityUtils.GetPrimaryKey(propertyAttr);
                                if (!string.IsNullOrEmpty(strPrimaryKey))
                                    propvalue = strPrimaryKey;
                            }
                        }

                        tableInfo.Id.Key = columnName;
                        tableInfo.Id.Value = propvalue;
                        tableInfo.PropToColumn.Put(propName, columnName);
                        breakForeach = true;
                    }
                }
                if (breakForeach) { breakForeach = false; continue; }
                tableInfo.Columns.Put(columnName, propvalue);
                tableInfo.PropToColumn.Put(propName, columnName);
            }

            return tableInfo;
        }

        public static string GetFindAllSql(TableInfo tableInfo)
        {
            StringBuilder sbColumns = new StringBuilder();

            tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);
            foreach (String key in tableInfo.Columns.Keys)
            {
                sbColumns.Append(key).Append(",");
            }

            sbColumns.Remove(sbColumns.ToString().Length - 1, 1);

            string strSql = "SELECT {0} FROM {1}";
            strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName);

            return strSql;
        }

        public static string GetFindByIdSql(TableInfo tableInfo)
        {
            StringBuilder sbColumns = new StringBuilder();

            if (tableInfo.Columns.ContainsKey(tableInfo.Id.Key))
                tableInfo.Columns[tableInfo.Id.Key] = tableInfo.Id.Value;
            else
                tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);

            foreach (String key in tableInfo.Columns.Keys)
            {
                sbColumns.Append(key).Append(",");
            }

            sbColumns.Remove(sbColumns.ToString().Length - 1, 1);

            string strSql = "SELECT {0} FROM {1} WHERE {2} = " + AdoHelper.DbParmChar + "{2}";
            strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName, tableInfo.Id.Key);

            return strSql;
        }

        public static string GetFindByPropertySql(TableInfo tableInfo)
        {
            StringBuilder sbColumns = new StringBuilder();

            tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);
            foreach (String key in tableInfo.Columns.Keys)
            {
                sbColumns.Append(key).Append(",");
            }

            sbColumns.Remove(sbColumns.ToString().Length - 1, 1);

            string strSql = "SELECT {0} FROM {1} WHERE {2} = " + AdoHelper.DbParmChar + "{2}";
            strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName, tableInfo.Id.Key);

            return strSql;
        }

        public static string GetInsertSql(TableInfo tableInfo)
        {
            StringBuilder sbColumns = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();

            if (tableInfo.Strategy != GenerationType.INDENTITY)
                tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);

            foreach (String key in tableInfo.Columns.Keys)
            {
                if (!string.IsNullOrEmpty(key.Trim()))
                {
                    Object value = tableInfo.Columns[key];
                    sbColumns.Append(key).Append(",");
                    sbValues.Append(AdoHelper.DbParmChar).Append(key).Append(",");
                }
            }

            sbColumns.Remove(sbColumns.ToString().Length - 1, 1);
            sbValues.Remove(sbValues.ToString().Length - 1, 1);

            string strSql = "INSERT INTO {0}({1}) VALUES({2})";
            strSql = string.Format(strSql, tableInfo.TableName, sbColumns.ToString(), sbValues.ToString());

            return strSql;
        }

        public static string GetUpdateSql(TableInfo tableInfo)
        {
            StringBuilder sbBody = new StringBuilder();

            foreach (String key in tableInfo.Columns.Keys)
            {
                Object value = tableInfo.Columns[key];

                sbBody.Append(key).Append("=").Append(AdoHelper.DbParmChar + key).Append(",");
            }

            sbBody.Remove(sbBody.ToString().Length - 1, 1);

            tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);

            string strSql = "update {0} set {1} where {2} =" + AdoHelper.DbParmChar + tableInfo.Id.Key;
            strSql = string.Format(strSql, tableInfo.TableName, sbBody.ToString(), tableInfo.Id.Key);

            return strSql;
        }

        public static string GetDeleteByIdSql(TableInfo tableInfo)
        {
            string strSql = "delete {0} where {1} =" + AdoHelper.DbParmChar + tableInfo.Id.Key;
            strSql = string.Format(strSql, tableInfo.TableName, tableInfo.Id.Key);

            return strSql;
        }

        public static void SetParameters(ColumnInfo columns, params IDbDataParameter[] parms)
        {
            int i = 0;
            foreach (string key in columns.Keys)
            {
                if (!string.IsNullOrEmpty(key.Trim()))
                {
                    parms[i].ParameterName = key;
                    parms[i].Value = columns[key];
                    i++;
                }
            }
        }

        public static bool IsCaseColumn(object attribute, DbOperateType dbOperateType)
        {
            if (attribute is ColumnAttribute)
            {
                ColumnAttribute columnAttr = attribute as ColumnAttribute;
                if (!columnAttr.IsInsert && dbOperateType == DbOperateType.INSERT)
                {
                    return true;
                }
                if (!columnAttr.IsUpdate && dbOperateType == DbOperateType.UPDATE)
                {
                    return true;
                }
            }

            return false;
        }
    }
}