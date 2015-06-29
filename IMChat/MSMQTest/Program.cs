using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MSMQTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //MessageQueue msq = MessageQueue.Create(@".\Private$\test2");
            //var msg = string.Format("send first message{0}", DateTime.Now);
            //msq.Send(msg);
            //Console.WriteLine(msg);
            //Console.ReadKey();

            ProducerMq.SingleToSingle("hello world！！！SINGLE");
            Console.WriteLine("消息发送。。。");
            Console.ReadKey();
        }
    }
}
