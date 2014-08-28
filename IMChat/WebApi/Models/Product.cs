using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;
using WebApi.Tool;

namespace WebApi.Models
{
    //[DataContract]
    /// <summary>
    /// 类型自定义转换
    /// </summary>
    [TypeConverter(typeof(ProductCon))]
    public class Product
    {
        public long ID { get; set; }

        [DataMember(Name = "PN")]
        public string Name { get; set; }

        [JsonIgnore]//此特性为忽略序列化字段
        public decimal Price { get; set; }

        public static bool TryParse(string s, out Product result)
        {
            result = null;

            var parts = s.Split(',');
            if (parts.Length != 3)
            {
                return false;
            }

            long id;
            decimal price;
            if (long.TryParse(parts[0], out id) &&
                decimal.TryParse(parts[2], out price))
            {
                result = new Product() { ID = id, Name = parts[1], Price = price };
                return true;
            }
            return false;
        }
    }
}