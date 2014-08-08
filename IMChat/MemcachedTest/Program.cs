using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memcached.ClientLibrary;

namespace MemcachedTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //程序池配置
            //Servers是memcached服务端开的地址和ip列表字符串
            string[] serverlist = { "127.0.0.1:11211" };

            SockIOPool pool = SockIOPool.GetInstance();

            pool.SetServers(serverlist);

            pool.InitConnections = 3;//初始的时候连接数
            pool.MinConnections = 3;//表示最小闲置连接数
            pool.MaxConnections = 5;//最大连接数

            pool.SocketConnectTimeout = 1000;//是socket连接超时时间
            pool.SocketTimeout = 3000;//


            pool.MaintenanceSleep = 30;//表示是否需要延时结束（最好设置为0，
            //如果设置延时的话那么就不能够立刻回收所有的资源，如果此时从新启动同样的资源分配，就会出现问题
            pool.Failover = true;//表示对于服务器出现问题时的自动修复

            pool.Nagle = false;//是TCP对于socket创建的算法
            //pool.Weights;//weights是上面服务器的权重，必须数量一致，否则权重无效
            //aliveCheck表示心跳检查，确定服务器的状态
            pool.Initialize();


            //客户端配置
            MemcachedClient mc = new MemcachedClient();
            mc.EnableCompression = false;//表示传输的时候是否压缩
            Console.WriteLine("Test");

            mc.Set("Name", "Chen");
            if (mc.KeyExists("Name"))
            {
                Console.WriteLine("已存在");
                Console.WriteLine(mc.Get("Name"));
            }


        }
    }
}
