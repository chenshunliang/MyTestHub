using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace WeiXin.Common
{
    /// <summary>
    /// 内存缓存帮助类
    /// </summary>
    public class MemoryCacheHelper
    {

        private static MemoryCache _mem = new MemoryCache("weixin");

        /// <summary>
        /// 获取/设置缓存的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key" />
        /// <param name="func"></param>
        /// <param name="sceonds"></param>
        /// <returns></returns>
        public static string GetCacheItem<T>(string key, Func<string> func, int sceonds)
        {
            object obj = _mem.Get(key);
            if (obj == null)
            {
                //进行设置
                string value = func.Invoke();
                _mem.Set(key, value, DateTimeOffset.Now.AddSeconds(sceonds));
                return value;
            }
            else
            {
                return obj.ToString();
            }
        }
    }
}