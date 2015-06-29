using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace MSMQTest
{
    public class CustmerMq
    {
        public static void InitCustmerMq()
        {

            string exchange = "ex1";
            string exchangeType = "direct";
            string routingKey = "m1";

            string serverAddress = "10.0.4.85:5672";
            ConnectionFactory cf = new ConnectionFactory();
            cf.HostName = "127.0.0.1";
            cf.Port = 1234;
            cf.UserName = "chen";
            cf.Password = "1qazxsw2";
            //cf.VirtualHost = "dnt_mq";
            cf.RequestedHeartbeat = 0;

            using (IConnection conn = cf.CreateConnection())
            {
                using (IModel ch = conn.CreateModel())
                {
                    //普通使用方式BasicGet
                    //noAck = true，不需要回复，接收到消息后，queue上的消息就会清除
                    //noAck = false，需要回复，接收到消息后，queue上的消息不会被清除，直到调用channel.basicAck(deliveryTag, false); queue上的消息才会被清除 而且，在当前连接断开以前，其它客户端将不能收到此queue上的消息
                    BasicGetResult res = ch.BasicGet("q1", false /*noAck*/);
                    if (res != null)
                    {
                        bool t = res.Redelivered;
                        t = true;
                        Console.WriteLine(System.Text.UTF8Encoding.UTF8.GetString(res.Body));
                        ch.BasicAck(res.DeliveryTag, false);
                    }
                    else
                    {
                        Console.WriteLine("No message！");
                    }
                }
            }
        }
    }
}
