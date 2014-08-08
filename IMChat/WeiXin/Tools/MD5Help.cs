using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WeiXin.Tools
{
    public class MD5Help
    {
        public static string MD5ForString(string str)
        {
            MD5 md = MD5.Create();
            byte[] bts = Encoding.UTF8.GetBytes(str);
            byte[] newbts = md.ComputeHash(bts);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < newbts.Length; i++)
            {
                sb.Append(newbts[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}