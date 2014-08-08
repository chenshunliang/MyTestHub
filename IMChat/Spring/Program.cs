using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;
using Spring.Context.Support;
using Spring.Core.IO;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Xml;

namespace Spring
{
    class Program
    {
        static void Main(string[] args)
        {
            ////实际物理路径
            //IResource input = new FileSystemResource(@"D:\Objects.xml");
            //IObjectFactory factory = new XmlObjectFactory(input);

            ////程序集下寻找配置文件
            string[] xmlFiles = new string[]　
            {
                "file://Objects.xml"
            };
            IPersonDao dao1 = IoCMethod(xmlFiles);
            IPersonDao dao2 = IoCMethod(xmlFiles);
            Console.WriteLine(IPersonDao.Equals(dao1, dao2));
            Console.ReadKey();

        }


        private static void NormalMethod()
        {
            IPersonDao dao = new PersonDao();
            dao.Save();
            Console.WriteLine("我是一般方法");
        }
        private static void FactoryMethod()
        {
            IPersonDao dao = DataAccess.CreatePersonDao();
            dao.Save();
            Console.WriteLine("我是工厂方法");
        }
        private static IPersonDao IoCMethod(string[] xmlFiles)
        {
            //IApplicationContext ctx = ContextRegistry.GetContext();
            IApplicationContext ctx = new XmlApplicationContext(xmlFiles);
            IObjectFactory factory = (IObjectFactory)ctx;
            IPersonDao dao = factory.GetObject("PersonDao") as IPersonDao;
            return dao;
        }
    }
}
