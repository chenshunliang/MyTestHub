using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            //RSAHelper.RSAKey rsa = RSAHelper.GetRASKey();
            //Console.WriteLine(rsa.PublicKey);
            //Console.WriteLine(rsa.PrivateKey);
            //RSACryptoServiceProvider.UseMachineKeyStore = true;
            ////声明一个指定大小的RSA容器
            //RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(1024);
            ////取得RSA容易里的各种参数
            ////RSAParameters p = rsaProvider.ExportParameters(true);
            ////Console.WriteLine(Convert.ToBase64String(p.Exponent));//公钥指数
            ////Console.WriteLine(Convert.ToBase64String(p.Modulus));//P，Q乘积n
            ////Console.WriteLine(Convert.ToBase64String(p.D));//私钥指数

            //RSACsharp rsa = new RSACsharp();
            //rsa.Initial();
            //Console.WriteLine(rsa.PublicKey);
            //Console.WriteLine(rsa.PrivateKey);

            //string str = "今天天气不错啊";
            //Console.WriteLine(rsa.EncryptData(str));
            //Console.WriteLine(rsa.DecryptData(rsa.EncryptData(str)));

            string sign = "name=chen&age=18&gender=man";
            //RSACryptoServiceProvider rsaProvide = new RSACryptoServiceProvider(1024);
            //RSAParameters rsaPamas = rsaProvide.ExportParameters(true);
            //string publicKey = rsaProvide.ToXmlString(false);
            //string privateKey = rsaProvide.ToXmlString(true);

            //RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

            //string str_Private_Key = Convert.ToBase64String(RSAalg.ExportCspBlob(true));
            //string str_Public_Key = Convert.ToBase64String(RSAalg.ExportCspBlob(false));

            ////签名
            //string signs = RSASign.HashAndSign(sign, str_Private_Key);


            //if (RSASign.VerifySignedHash(sign, signs, str_Public_Key))
            //    Console.WriteLine("验签成功");
            //Console.WriteLine(signs);

            string key = "";//60uqmp4ijAW7M8qcZ9whEw==
            string IV = "";//EUPD2Fa8
            //DESHelper.CreateKeyAndIV(out key, out IV);
            string cer = DESHelper.Encrypt(sign, "EUPD2Fa8");
            Console.WriteLine(cer);

            string pfx = DESHelper.Decrypt(cer, "EUPD2Fa8");
            Console.WriteLine(pfx);

            Console.ReadKey();
        }
    }

    class RSAHelper
    {
        /// <summary>
        /// RSA加密的密匙结构  公钥和私匙
        /// </summary>
        public struct RSAKey
        {
            public string PublicKey { get; set; }
            public string PrivateKey { get; set; }
        }

        #region 得到RSA的解谜的密匙对
        /// <summary>
        /// 得到RSA的解谜的密匙对
        /// </summary>
        /// <returns></returns>
        public static RSAKey GetRASKey()
        {
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            //声明一个指定大小的RSA容器
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(1024);
            //取得RSA容易里的各种参数
            RSAParameters p = rsaProvider.ExportParameters(true);
            return new RSAKey()
            {
                PublicKey = ComponentKey(p.Exponent, p.Modulus),
                PrivateKey = ComponentKey(p.D, p.Modulus)
            };
        }
        #endregion
        #region 组合解析密匙
        /// <summary>
        /// 组合成密匙字符串
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        private static string ComponentKey(byte[] b1, byte[] b2)
        {
            List<byte> list = new List<byte>();
            //在前端加上第一个数组的长度值 这样今后可以根据这个值分别取出来两个数组
            list.Add((byte)b1.Length);
            list.AddRange(b1);
            list.AddRange(b2);
            byte[] b = list.ToArray<byte>();
            return Convert.ToBase64String(b);
        }

        /// <summary>
        /// 解析密匙
        /// </summary>
        /// <param name="key">密匙</param>
        /// <param name="b1">RSA的相应参数1</param>
        /// <param name="b2">RSA的相应参数2</param>
        private static void ResolveKey(string key, out byte[] b1, out byte[] b2)
        {
            //从base64字符串 解析成原来的字节数组
            byte[] b = Convert.FromBase64String(key);
            //初始化参数的数组长度
            b1 = new byte[b[0]];
            b2 = new byte[b.Length - b[0] - 1];
            //将相应位置是值放进相应的数组
            for (int n = 1, i = 0, j = 0; n < b.Length; n++)
            {
                if (n <= b[0])
                {
                    b1[i++] = b[n];
                }
                else
                {
                    b2[j++] = b[n];
                }
            }
        }
        #endregion
    }
}
