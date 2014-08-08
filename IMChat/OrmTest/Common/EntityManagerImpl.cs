using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using OrmTest.Entity;

namespace OrmTest.Common
{
    public class EntityManagerImpl : IEntityManager
    {
        IDbTransaction transaction = null;

        #region 将实体数据保存到数据库
        public int Save<T>(T entity)
        {
            TableInfo tableInfo = DbEntityUtils.GetTableInfo(entity, DbOperateType.INSERT);

            String strSql = DbEntityUtils.GetInsertSql(tableInfo);

            IDbDataParameter[] parms = DbFactory.CreateDbParameters(tableInfo.Columns.Count);

            DbEntityUtils.SetParameters(tableInfo.Columns, parms);

            object val = AdoHelper.ExecuteNonQuery(transaction, CommandType.Text, strSql, parms);

            return Convert.ToInt32(val);
        }
        #endregion

        #region 将实体数据修改到数据库
        public int Update<T>(T entity)
        {
            TableInfo tableInfo = DbEntityUtils.GetTableInfo(entity, DbOperateType.UPDATE);

            String strSql = DbEntityUtils.GetUpdateSql(tableInfo);

            IDbDataParameter[] parms = DbFactory.CreateDbParameters(tableInfo.Columns.Count);

            DbEntityUtils.SetParameters(tableInfo.Columns, parms);

            object val = AdoHelper.ExecuteNonQuery(transaction, CommandType.Text, strSql, parms);

            return Convert.ToInt32(val);
        }
        #endregion

        #region 删除实体对应数据库中的数据
        public int Remove<T>(T entity)
        {
            TableInfo tableInfo = DbEntityUtils.GetTableInfo(entity, DbOperateType.DELETE);

            String strSql = DbEntityUtils.GetDeleteByIdSql(tableInfo);

            IDbDataParameter[] parms = DbFactory.CreateDbParameters(1);
            parms[0].ParameterName = tableInfo.Id.Key;
            parms[0].Value = tableInfo.Id.Value;

            object val = AdoHelper.ExecuteNonQuery(transaction, CommandType.Text, strSql, parms);

            return Convert.ToInt32(val);
        }
        #endregion

        #region 根据主键id删除实体对应数据库中的数据
        public int Remove<T>(object id) where T : new()
        {
            TableInfo tableInfo = DbEntityUtils.GetTableInfo(new T(), DbOperateType.DELETE);

            String strSql = DbEntityUtils.GetDeleteByIdSql(tableInfo);

            IDbDataParameter[] parms = DbFactory.CreateDbParameters(1);
            parms[0].ParameterName = tableInfo.Id.Key;
            parms[0].Value = id;

            object val = AdoHelper.ExecuteNonQuery(transaction, CommandType.Text, strSql, parms);

            return Convert.ToInt32(val);
        }
        #endregion

        #region 查询实体对应表的所有数据
        public List<T> FindAll<T>() where T : new()
        {
            List<T> listArr = new List<T>();
            PropertyInfo[] properties = ReflectionUtils.GetProperties(new T().GetType());

            TableInfo tableInfo = DbEntityUtils.GetTableInfo(new T(), DbOperateType.SELECT);

            String strSql = DbEntityUtils.GetFindAllSql(tableInfo);

            IDataReader sdr = null;
            try
            {
                sdr = AdoHelper.ExecuteReader(AdoHelper.ConnectionString, CommandType.Text, strSql);
                while (sdr.Read())
                {
                    T entity = new T();
                    foreach (PropertyInfo property in properties)
                    {
                        String name = tableInfo.PropToColumn[property.Name].ToString();
                        ReflectionUtils.SetPropertyValue(entity, property, sdr[name]);
                    }

                    listArr.Add(entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sdr != null) sdr.Close();
            }

            return listArr;
        }
        #endregion

        #region 通过自定义SQL语句查询数据
        public List<T> FindBySql<T>(string strSql) where T : new()
        {
            List<T> listArr = new List<T>();
            Type classType = new T().GetType();
            PropertyInfo[] properties = ReflectionUtils.GetProperties(classType);

            IDataReader sdr = null;
            try
            {
                sdr = AdoHelper.ExecuteReader(AdoHelper.ConnectionString, CommandType.Text, strSql);
                while (sdr.Read())
                {
                    T entity = new T();
                    foreach (PropertyInfo property in properties)
                    {
                        ReflectionUtils.SetPropertyValue(entity, property, sdr[property.Name]);
                    }

                    listArr.Add(entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sdr != null) sdr.Close();
            }

            return listArr;
        }
        #endregion

        #region 通过主键ID查询数据
        public List<T> FindById<T>(object id) where T : new()
        {
            List<T> listArr = new List<T>();
            PropertyInfo[] properties = ReflectionUtils.GetProperties(new T().GetType());

            TableInfo tableInfo = DbEntityUtils.GetTableInfo(new T(), DbOperateType.SELECT);

            String strSql = DbEntityUtils.GetFindByIdSql(tableInfo);

            IDbDataParameter[] parms = DbFactory.CreateDbParameters(1);
            parms[0].ParameterName = tableInfo.Id.Key;
            parms[0].Value = id;

            IDataReader sdr = null;
            try
            {
                sdr = AdoHelper.ExecuteReader(AdoHelper.ConnectionString, CommandType.Text, strSql, parms);
                while (sdr.Read())
                {
                    T entity = new T();
                    foreach (PropertyInfo property in properties)
                    {
                        String name = tableInfo.PropToColumn[property.Name].ToString();
                        ReflectionUtils.SetPropertyValue(entity, property, sdr[name]);
                    }

                    listArr.Add(entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sdr != null) sdr.Close();
            }

            return listArr;
        }
        #endregion

        #region Transaction 注入事物对象属性
        public IDbTransaction Transaction
        {
            get
            {
                return transaction;
            }
            set
            {
                transaction = value;
            }
        }
        #endregion
    }
}