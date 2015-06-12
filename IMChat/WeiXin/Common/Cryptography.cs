﻿using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace WeiXin.Common
{
    public class Cryptography
    {
        public static UInt32 HostToNetworkOrder(UInt32 inval)
        {
            UInt32 outval = 0;
            for (int i = 0; i < 4; i++)
                outval = (outval << 8) + ((inval >> (i * 8)) & 255);
            return outval;
        }

        public static Int32 HostToNetworkOrder(Int32 inval)
        {
            Int32 outval = 0;
            for (int i = 0; i < 4; i++)
                outval = (outval << 8) + ((inval >> (i * 8)) & 255);
            return outval;
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="input">密文</param>
        /// <param name="encodingAesKey"></param>
        /// <param name="corpid"></param>
        /// <returns></returns>
        public static string AES_decrypt(String input, string encodingAesKey, ref string corpid)
        {
            if (input == null) throw new ArgumentNullException("input");
            if (encodingAesKey == null) throw new ArgumentNullException("encodingAesKey");
            byte[] key = Convert.FromBase64String(encodingAesKey + "=");
            byte[] iv = new byte[16];
            Array.Copy(key, iv, 16);
            byte[] btmpMsg = AES_decrypt(input, iv, key);

            int len = BitConverter.ToInt32(btmpMsg, 16);
            len = IPAddress.NetworkToHostOrder(len);


            byte[] bMsg = new byte[len];
            byte[] bCorpid = new byte[btmpMsg.Length - 20 - len];
            Array.Copy(btmpMsg, 20, bMsg, 0, len);
            Array.Copy(btmpMsg, 20 + len, bCorpid, 0, btmpMsg.Length - 20 - len);
            string oriMsg = Encoding.UTF8.GetString(bMsg);
            corpid = Encoding.UTF8.GetString(bCorpid);


            return oriMsg;
        }

        public static String AES_encrypt(String input, string encodingAesKey, string corpid)
        {
            if (input == null) throw new ArgumentNullException("input");
            byte[] key = Convert.FromBase64String(encodingAesKey + "=");
            byte[] iv = new byte[16];
            Array.Copy(key, iv, 16);
            string randcode = CreateRandCode(16);
            byte[] bRand = Encoding.UTF8.GetBytes(randcode);
            byte[] bCorpid = Encoding.UTF8.GetBytes(corpid);
            byte[] btmpMsg = Encoding.UTF8.GetBytes(input);
            byte[] bMsgLen = BitConverter.GetBytes(HostToNetworkOrder(btmpMsg.Length));
            byte[] bMsg = new byte[bRand.Length + bMsgLen.Length + bCorpid.Length + btmpMsg.Length];

            Array.Copy(bRand, bMsg, bRand.Length);
            Array.Copy(bMsgLen, 0, bMsg, bRand.Length, bMsgLen.Length);
            Array.Copy(btmpMsg, 0, bMsg, bRand.Length + bMsgLen.Length, btmpMsg.Length);
            Array.Copy(bCorpid, 0, bMsg, bRand.Length + bMsgLen.Length + btmpMsg.Length, bCorpid.Length);

            return AES_encrypt(bMsg, iv, key);

        }
        private static string CreateRandCode(int codeLen)
        {
            string codeSerial = "2,3,4,5,6,7,a,c,d,e,f,h,i,j,k,m,n,p,r,s,t,A,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,U,V,W,X,Y,Z";
            if (codeLen == 0)
            {
                codeLen = 16;
            }
            string[] arr = codeSerial.Split(',');
            string code = "";
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < codeLen; i++)
            {
                int randValue = rand.Next(0, arr.Length - 1);
                code += arr[randValue];
            }
            return code;
        }

        private static String AES_encrypt(byte[] input, byte[] iv, byte[] key)
        {
            if (input == null) throw new ArgumentNullException("input");
            if (iv == null) throw new ArgumentNullException("iv");
            var aes = new RijndaelManaged();
            //秘钥的大小，以位为单位
            aes.KeySize = 256;
            //支持的块大小
            aes.BlockSize = 128;
            //填充模式
            //aes.Padding = PaddingMode.PKCS7;
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;
            aes.Key = key;
            aes.IV = iv;
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff;

            #region 自己进行PKCS7补位，用系统自己带的不行
            byte[] msg = new byte[input.Length + 32 - input.Length % 32];
            Array.Copy(input, msg, input.Length);
            byte[] pad = Kcs7Encoder(input.Length);
            Array.Copy(pad, 0, msg, input.Length, pad.Length);
            #endregion

            #region 注释的也是一种方法，效果一样
            //ICryptoTransform transform = aes.CreateEncryptor();
            //byte[] xBuff = transform.TransformFinalBlock(msg, 0, msg.Length);
            #endregion

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    cs.Write(msg, 0, msg.Length);
                }
                xBuff = ms.ToArray();
            }

            string output = Convert.ToBase64String(xBuff);
            return output;
        }

        private static byte[] Kcs7Encoder(int textLength)
        {
            const int blockSize = 32;
            // 计算需要填充的位数
            int amountToPad = blockSize - (textLength % blockSize);
            if (amountToPad == 0)
            {
                amountToPad = blockSize;
            }
            // 获得补位所用的字符
            char padChr = Chr(amountToPad);
            string tmp = "";
            for (int index = 0; index < amountToPad; index++)
            {
                tmp += padChr;
            }
            return Encoding.UTF8.GetBytes(tmp);
        }
        /**
         * 将数字转化成ASCII码对应的字符，用于对明文进行补码
         * 
         * @param a 需要转化的数字
         * @return 转化得到的字符
         */
        static char Chr(int a)
        {

            byte target = (byte)(a & 0xFF);
            return (char)target;
        }
        private static byte[] AES_decrypt(String input, byte[] iv, byte[] key)
        {
            if (iv == null) throw new ArgumentNullException("iv");
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.Key = key;
            aes.IV = iv;
            var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] xBuff;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(input);
                    byte[] msg = new byte[xXml.Length + 32 - xXml.Length % 32];
                    Array.Copy(xXml, msg, xXml.Length);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = Decode2(ms.ToArray());
            }
            return xBuff;
        }
        private static byte[] Decode2(byte[] decrypted)
        {
            int pad = decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
            {
                pad = 0;
            }
            byte[] res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }
    }
}