using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgressSync
{
    class Program
    {
        static void Main(string[] args)
        {
            string MutexName = "InterProcessSyncName";
            Mutex SyncNamed;     //声明一个已命名的互斥对象
            try
            {
                SyncNamed = Mutex.OpenExisting(MutexName);       //如果此命名互斥对象已存在则请求打开
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                SyncNamed = new Mutex(false, MutexName);         //如果初次运行没有已命名的互斥对象则创建一个
            }
            Task MulTesk = new Task
                (
                    () =>                  //多任务并行计算中的匿名方法，用委托也可以
                    {
                        for (; ; )         //为了效果明显而设计
                        {
                            Console.WriteLine("当前进程等待获取互斥访问权......");
                            SyncNamed.WaitOne();
                            Console.WriteLine("获取互斥访问权，访问资源完毕，按回车释放互斥资料访问权.");
                            Console.ReadLine();
                            SyncNamed.ReleaseMutex();
                            Console.WriteLine("已释放互斥访问权。");
                        }
                    }
                );
            MulTesk.Start();
            MulTesk.Wait();
        }

    }
}
