using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public class RSACsharp
    {
        public string PublicKey, PrivateKey;
        RSACryptoServiceProvider rsaProvider;
        public void Initial()
        {
            //声明一个RSA算法的实例，由RSACryptoServiceProvider类型的构造函数指定了密钥长度为1024位
            //实例化RSACryptoServiceProvider后，RSACryptoServiceProvider会自动生成密钥信息。
            rsaProvider = new RSACryptoServiceProvider(1024);
            //将RSA算法的公钥导出到字符串PublicKey中，参数为false表示不导出私钥
            PublicKey = rsaProvider.ToXmlString(false);
            //将RSA算法的私钥导出到字符串PrivateKey中，参数为true表示导出私钥
            PrivateKey = rsaProvider.ToXmlString(true);
        }


        public byte[] EncryptData(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str); //Convert.FromBase64String(str);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            //将公钥导入到RSA对象中，准备加密；
            rsa.FromXmlString(PublicKey);
            //对数据data进行加密，并返回加密结果；
            //第二个参数用来选择Padding的格式
            return rsa.Encrypt(data, false);
        }

        public string DecryptData(byte[] data)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            //将私钥导入RSA中，准备解密；
            rsa.FromXmlString(PrivateKey);
            //对数据进行解密，并返回解密结果；
            byte[] bts = rsa.Decrypt(data, false);
            //return Convert.ToBase64String(bts);
            return Encoding.UTF8.GetString(bts);
        }

        public byte[] Sign(byte[] data)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            //导入私钥，准备签名
            rsa.FromXmlString(PrivateKey);
            //将数据使用MD5进行消息摘要，然后对摘要进行签名并返回签名数据
            return rsa.SignData(data, "MD5");
        }

        public bool Verify(byte[] data, byte[] Signature)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            //导入公钥，准备验证签名
            rsa.FromXmlString(PublicKey);
            //返回数据验证结果
            return rsa.VerifyData(data, "MD5", Signature);
        }

    }
}
