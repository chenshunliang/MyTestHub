using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Test
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
            cf.Port = 5672;
            cf.UserName = "chen";
            cf.Password = "1qazxsw2";
            //cf.VirtualHost = "dnt_mq";
            cf.RequestedHeartbeat = 0;

            using (IConnection conn = cf.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //普通使用方式BasicGet
                    //noAck = true，不需要回复，接收到消息后，queue上的消息就会清除
                    //noAck = false，需要回复，接收到消息后，queue上的消息不会被清除，直到调用channel.basicAck(deliveryTag, false); queue上的消息才会被清除 而且，在当前连接断开以前，其它客户端将不能收到此queue上的消息
                    channel.QueueDeclare("SingleToSingle", false, false, false, null);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("SingleToSingle", true, consumer);

                    Console.WriteLine(" waiting for message.");
                    while (true)
                    {
                        var ea = consumer.Queue.Dequeue();

                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Received {0}", message);
                    }
                }
            }
        }
    }
}
