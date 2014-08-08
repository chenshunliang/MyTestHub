using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace IMChat.Common
{
    //安全密码机制
    public class PwdSalt
    {
        public const string PasswordHashAlgorithmName = "SHA1";//hash值加密

        //hash加密
        public static string EncodePassword(string password, string salt)
        {
            byte[] src = Encoding.Unicode.GetBytes(password);
            byte[] saltbuf = Convert.FromBase64String(salt);
            byte[] dst = new byte[saltbuf.Length + src.Length];
            byte[] inArray = null;
            Buffer.BlockCopy(saltbuf, 0, dst, 0, saltbuf.Length);
            Buffer.BlockCopy(src, 0, dst, saltbuf.Length, src.Length);
            //哈希散列值
            HashAlgorithm algorithm = HashAlgorithm.Create(PasswordHashAlgorithmName);
            inArray = algorithm.ComputeHash(dst);

            return Convert.ToBase64String(inArray);
        }

        //MD5加密
        public static string EncodePwdToMD5(string password, string salt)
        {
            byte[] src = Encoding.Unicode.GetBytes(password);
            byte[] saltbuf = Convert.FromBase64String(salt);
            byte[] dst = new byte[saltbuf.Length + src.Length];
            byte[] inArray = null;
            Buffer.BlockCopy(saltbuf, 0, dst, 0, saltbuf.Length);
            Buffer.BlockCopy(src, 0, dst, saltbuf.Length, src.Length);
            //哈希散列值
            HashAlgorithm algorithm = HashAlgorithm.Create("MD5");
            inArray = algorithm.ComputeHash(dst);

            return Convert.ToBase64String(inArray);
        }

        //MD5普通加密
        public static string EncodePwdToMD5(string password)
        {
            byte[] src = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length];
            byte[] inArray = null;
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            //哈希散列值
            HashAlgorithm algorithm = HashAlgorithm.Create("MD5");
            inArray = algorithm.ComputeHash(dst);

            return Convert.ToBase64String(inArray);
        }

        //强随机值
        public static string GenerateSalt()
        {
            byte[] data = new byte[0x10];
            //生成强随机数
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }
    }
}