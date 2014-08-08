using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public static class RSASign
    {
        //对数据签名
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str_DataToSign">待签名数据</param>
        /// <param name="str_Private_Key">私钥</param>
        /// <returns></returns>
        public static string HashAndSign(string str_DataToSign, string str_Private_Key)
        {
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] DataToSign = ByteConverter.GetBytes(str_DataToSign);
            try
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(1024);
                byte[] bts = Convert.FromBase64String(str_Private_Key);
                RSAalg.ImportCspBlob(bts);
                byte[] signedData = RSAalg.SignData(DataToSign, new SHA1CryptoServiceProvider());
                string str_SignedData = Convert.ToBase64String(signedData);
                return str_SignedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //验证签名
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str_DataToVerify">待验签数据</param>
        /// <param name="str_SignedData">签名串</param>
        /// <param name="str_Public_Key">公钥</param>
        /// <returns></returns>
        public static bool VerifySignedHash(string str_DataToVerify, string str_SignedData, string str_Public_Key)
        {
            byte[] SignedData = Convert.FromBase64String(str_SignedData);

            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] DataToVerify = ByteConverter.GetBytes(str_DataToVerify);
            try
            {
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(1024);
                RSAalg.ImportCspBlob(Convert.FromBase64String(str_Public_Key));

                return RSAalg.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);

            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
    }
}
