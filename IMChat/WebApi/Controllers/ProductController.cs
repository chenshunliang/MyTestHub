using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    public class ProductController : ApiController
    {
        #region Old
        //Product[] pros = new Product[] { 
        //new Product(){ ID=1, Name="手机", Price=1200.00m},
        //new Product(){ID=2,Name="笔记本", Price=3500.00m},
        //new Product(){ID=3,Name="汽车", Price=120000.00m}
        //};

        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return pros;
        //}

        //public string GetProNameById(int id)
        //{
        //    if (id >= pros.Length || id < 0)
        //        return "无此产品";
        //    else
        //    {
        //        return pros[id].Name;
        //    }
        //} 
        #endregion

        static readonly IProductRepository product = new ProductRepository();

        /// <summary>
        /// 获取所有产品，约定Get方法
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetAllProduct()
        {
            return product.GetAll();
        }

        /// <summary>
        /// 通过Id查找产品，约定Get方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        {
            return product.Get(id);
        }

        /// <summary>
        /// 通过产品名称查找产品，约定Get方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetProductsByName(string name)
        {
            return product.GetAll().Where(p => p.Name == name);
        }

        /// <summary>
        /// 添加产品，Post方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public HttpResponseMessage PostProduct(Product item)
        {
            item = product.Add(item);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.ID });
            response.Headers.Location = new Uri(uri);
            return response;
        }


        /// <summary>
        /// 更新产品，Put方法
        /// </summary>
        /// <param name="item"></param>
        public void PutProduct(Product item)
        {
            product.Update(item);
        }

        /// <summary>
        /// 删除产品，Delete方法
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            product.Remove(id);
        }
    }
}
