using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient
{
    class Program
    {
        static void Main(string[] args)
        {

            //client配置
            //WebRequestHandler handler = new WebRequestHandler()
            //{
            //    AllowAutoRedirect = false,   //允许自动重连接
            //    UseProxy = false    //使用代理
            //};
            //HttpClient client = new HttpClient(handler);


            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:2376/");
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage response = client.GetAsync("api/product/1").Result;
            ////if (response.IsSuccessStatusCode)
            ////{
            ////    var products = response.Content.ReadAsAsync<Product>().Result;
            ////    //foreach (var p in products)
            ////    //{
            ////    //    Console.WriteLine("{0}\t{1};\t{2}", p.Name, p.Price, p.ID);
            ////    //}

            ////    Console.WriteLine("{0}\t{1};\t{2}", products.Name, products.Price, products.ID);
            ////}
            ////else
            ////{
            ////    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            ////}


            ////HTTP Post请求
            //try
            //{
            //    var gizmo = new Product() { Name = "Gizmo", Price = 100, ID = 1 };
            //    Uri gizmoUri = null;

            //    response = client.PostAsJsonAsync("api/products", gizmo).Result;
            //    //如果状态码不是200~299  抛异常
            //    response.EnsureSuccessStatusCode();
            //    //IsSuccessStatusCode 200~299 为true
            //    if (response.IsSuccessStatusCode)
            //    {
            //        gizmoUri = response.Headers.Location;
            //    }
            //    else
            //    {
            //        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            //    }

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    Console.ReadKey();
            //}


            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes("123456");
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length - 1; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }

            Console.WriteLine(str);

            Console.ReadKey();
        }
    }
}
