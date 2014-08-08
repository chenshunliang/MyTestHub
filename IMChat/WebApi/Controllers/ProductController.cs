using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProductController : ApiController
    {
        Product[] pros = new Product[] { 
        new Product(){ ID=1, Name="手机", Price=1200.00m},
        new Product(){ID=2,Name="笔记本", Price=3500.00m},
        new Product(){ID=3,Name="汽车", Price=120000.00m}
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return pros;
        }

        public string GetProNameById(int id)
        {
            if (id >= pros.Length || id < 0)
                return "无此产品";
            else
            {
                return pros[id].Name;
            }
        }
    }
}
