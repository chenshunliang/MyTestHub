using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> proList = new List<Product>();

        private int index = 0;

        public ProductRepository()
        {
            Add(new Product() { ID = 1, Name = "手机", Price = 1200.00m });
            Add(new Product() { ID = 2, Name = "笔记本", Price = 3500.00m });
            Add(new Product() { ID = 3, Name = "汽车", Price = 120000.00m });
        }

        public IEnumerable<Models.Product> GetAll() 
        {
            return proList;
        }

        
        public Models.Product Get(int id)
        {
            if (id >= proList.Count || id < 0)
            {
                //响应消息
                HttpResponseMessage msg = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new StringContent("this product is not found"),
                    ReasonPhrase = "",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
                throw new HttpResponseException(msg);
            }
            else
            {
                return proList[id];
            }
        }

        public Models.Product Add(Models.Product item)
        {
            proList.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            proList.Remove(proList.Where(p => p.ID == id).FirstOrDefault());
        }

        public bool Update(Models.Product item)
        {
            var pro = proList.Where(m => m.ID == item.ID).FirstOrDefault();
            if (pro != null)
            {
                if (proList.Remove(pro))
                {
                    proList.Add(pro);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}