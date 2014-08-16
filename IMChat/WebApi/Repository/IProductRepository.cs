using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Repository
{
    /// <summary>
    /// 产品仓储
    /// </summary>
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product Get(int id);

        Product Add(Product item);

        void Remove(int id);

        bool Update(Product item);
    }
}