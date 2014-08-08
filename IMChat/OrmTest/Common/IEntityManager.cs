using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OrmTest.Common
{
    public interface IEntityManager
    {
        //事物
        IDbTransaction Transaction { get; set; }

        //查询表所有数据 
        List<T> FindAll<T>() where T : new();

        //自定义SQL查询 
        List<T> FindBySql<T>(string strSql) where T : new();

        //通过主键ID查询 
        List<T> FindById<T>(object id) where T : new();

        //新增
        int Save<T>(T entity);

        //修改
        int Update<T>(T entity);

        //删除
        int Remove<T>(T entity);

        //根据ID删除数据
        int Remove<T>(object id) where T : new();
    }
}