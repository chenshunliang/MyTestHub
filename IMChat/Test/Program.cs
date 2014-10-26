using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using System.Data.SqlClient;

namespace Test
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string newstr = "";

            //StreamReader sr = new StreamReader(@"d:\text.txt");
            //while (!sr.EndOfStream)
            //{
            //    string str = sr.ReadLine();
            //    string[] strs = str.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            //    newstr += "insert into dbo.Banks (BanksCode,BanksName) values ('" + strs[0] + "',N'" + strs[1] + "');\r\n";
            //}

            //Console.WriteLine(newstr);
            //Console.ReadKey();


            Console.WriteLine(Guid.NewGuid().ToString().Replace("-", "+"));
            Console.ReadKey();

            //PropertyInfo[] properties = new Student().GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //foreach (PropertyInfo item in properties)
            //{
            //    Console.WriteLine(item.Name);
            //}
            //Console.WriteLine(new Student().GetType().FullName);
            //Console.WriteLine(new Student().GetType().GetCustomAttributes(false));
            //Console.ReadKey();

            //Console.WriteLine(DateTime.Now);
            //Console.WriteLine(DateTime.Now.AddMinutes(20));
            //Console.ReadKey();

            //string str = "dadaa\r\ndfsdf\r\ndsfsdf\r\ndasd";
            //Console.WriteLine(str);
            //Console.WriteLine(str.Replace("\r\n", ""));
            //Console.ReadKey();
            //string str = "HyOo0yNiDFRYDLOKN9XHhDwHxSR3PrIr61fWkRXwdpMBn7qdxY5PkloRPwiNOAVrFrCQiaLTNCd0+FR1G4NqBfKI/z1QanE1nVmgw86WRWPr+RgSR1ilIV5FWT9SWQ6lZTYEcDRAJ59HpPOTsj49UJAWauHNY1AynMRDk7CBvlo=";
            //try
            //{
            //    //byte[] bts = new SHA1Managed().ComputeHash(Encoding.Default.GetBytes("amount=11&amt_type=RMB&charset=GBK&media_id=15810685261&mer_date=20131224&mer_id=25018&order_id=0080#&pay_date=20131224&pay_type=MOBILEBANKCARD&service=pay_result_notify&settle_date=20131224&trade_no=1399241126326062&trade_state=TRADE_SUCCESS&version=4.0"));
            //    //Console.WriteLine(Convert.ToBase64String(bts));
            //    //Convert.FromBase64String(str);
            //    //Console.WriteLine(str.Length);
            //    //Console.WriteLine(str.Length / 4.00);
            //    //Console.WriteLine("成功");
            //    //Console.WriteLine(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            //    //Analyzer analyzer = new PanGuAnalyzer();
            //    //TokenStream tokenStream = analyzer.TokenStream("", new StringReader(Console.ReadLine()));
            //    //Lucene.Net.Analysis.Token token = null;
            //    //while (tokenStream.IncrementToken())
            //    ////while ((token = tokenStream.Next()) != null)
            //    //{
            //    //    //Console.WriteLine(token.TermText());
            //    //    Console.WriteLine(tokenStream.ToString());
            //    //}
            //    //Console.WriteLine("============================================");
            //    //while ((token = tokenStream.Next()) != null)
            //    //{
            //    //    Console.WriteLine(token.TermText());
            //    //}

            //    //Console.WriteLine(XML.Get().Count());
            //    //foreach (XElement item in XML.Get())
            //    //{
            //    //    Console.WriteLine(item.Name);
            //    //}

            //    double a = 1 << 1 + 0;
            //    Console.WriteLine(a);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            Console.WriteLine(EncodePwdToMD5("abc", "a"));

            Console.ReadKey();
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

        /// <summary>
        /// 输出参数
        /// </summary>
        public static void ReturnParameter()
        {
            SqlParameter para = new SqlParameter("@ReturnValue", System.Data.SqlDbType.BigInt);
            para.Direction = System.Data.ParameterDirection.Output;
        }
    }

    public static class XML
    {
        public static IEnumerable<XElement> Get()
        {
            XDocument xdoc = XDocument.Load(@"C:\all.xml");
            XElement element = xdoc.Root;
            //return element.Descendants();

            return element.Elements();
        }
    }
}
